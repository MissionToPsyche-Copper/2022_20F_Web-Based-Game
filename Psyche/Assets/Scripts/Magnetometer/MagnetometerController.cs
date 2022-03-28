using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class MagnetometerController : MonoBehaviour
{
    public GameObject fieldObject;
    public GameObject target; //This gets attached to the player
    public GameObject effect; //this is attached to the target

    [Header("======= Main Field =========")]
    [Range(1, 200)]
    public int pivotDistance = 60;
    [Range(30, 200)]
    public float orbitRadius; //periapsis distance also called r1
    private Transform mainFieldsHolder;

    [Header("======= Anomalies =========")]
    [MinMaxRange(0, 100, order = 1)]
    [Tooltip("Chance to spawn a new surface anomally.\n")]
    public RangedInt spawnChance = new RangedInt(50, 75);
    private int spawnVal;
    [MinMaxRange(0.0f, 20.0f, order = 2)]
    [Tooltip("How long an anomally will appear before disappearing")]
    public RangedFloat spawnLifetime = new RangedFloat(3.0f, 7.0f);
    private float currentTime = 0;
    [MinMaxRange(0.0f, 20.0f, order = 2)]
    [Tooltip("The time interval (in seconds) between each spawn window")]
    public RangedFloat spawnInterval = new RangedFloat(3.0f, 7.0f);
    [MinMaxRange(10.0f, 50.0f)]
    [Tooltip("Adjusts the ranged of sizes that target anomallies will appear to be.\nThe size of the anomally also plays into how much points they are worth when scanned (smaller size = more points)")]
    public RangedFloat spawnSizeRange = new RangedFloat(3.0f, 6.0f);
    [MinMaxRange(0.1f, 3.0f)]
    [Tooltip("Adjusts the speed that anomalies will spin at.")]
    public RangedFloat spawnRotSpeed = new RangedFloat(0.5f, 1.0f);
    [MinMaxRange(0.1f, 1.0f)]
    [Tooltip("Adjusts the alpha that anomalies will appear at.")]
    public RangedFloat spawnAlpha = new RangedFloat(0.5f, 0.9f);

    private float intervalVal;
    private Transform anomaliesHolder;


    [Header("======= Scoring =========")]
    [SerializeField] private float baseScoreMod = 0.1f;
    [SerializeField] private float bonusScoreAdjust = 1.0f;
    [ReadOnly]
    public float bonusScoreMod = 0.0f;


    [Header("======= Debug =========")]

    [ReadOnly]
    [SerializeField] private bool inField = false;
    public static bool toolActive = false;




    // Start is called before the first frame update
    void Start()
    {
        anomaliesHolder = transform.Find("Anomalies");
        mainFieldsHolder = transform.Find("MainFields");

        //Send target object to the player object
        target.transform.position = LevelController.player.transform.position;
        target.transform.parent = LevelController.player.transform;
        fieldObject.SetActive(false);

        //Send anomalies holder to asteroid
        anomaliesHolder.parent = LevelController.mainAsteroid.transform;

        CreateMainFields();
        intervalVal = Random.Range(spawnInterval.Min, spawnInterval.Max);
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            toolActive = !toolActive;
            effect.SetActive(toolActive);            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LevelController.gameEnd)
            return;

        currentTime += Time.deltaTime;
        if (currentTime > intervalVal)
        {
            spawnVal = Random.Range(spawnChance.Min, spawnChance.Max);
            if (Random.value * 100.0f <= spawnVal)
            {
                CreateAnomallyField();
            }
            intervalVal = Random.Range(spawnInterval.Min, spawnInterval.Max);
            currentTime = 0;
        }


        if (toolActive)
        {
            if(ShipControl.isThrusting || !ShipControl.resources.CanUsePower())
            {
                toolActive = false;
                effect.SetActive(toolActive);
                return;
            }
            else if (inField)
            {
                LevelController.levelRoot.ScoreMagnetometer(Time.deltaTime * (baseScoreMod + bonusScoreMod));
                ShipControl.resources.UsePower(Constants.Ship.Resources.PowerUse.Magnetometer * Time.deltaTime);
            }
        }
    }

    private void CreateAnomallyField()
    {
        GameObject anomally = Instantiate(fieldObject);

        MagneticField anomallyField = anomally.GetComponent<MagneticField>();

        float alpha = Random.Range(spawnAlpha.Min, spawnAlpha.Max);
        float rotSpeed = Random.Range(spawnRotSpeed.Min, spawnRotSpeed.Max);
        float major = Random.Range(spawnSizeRange.Min, spawnSizeRange.Max);
        float minor = Random.Range(spawnSizeRange.Min, spawnSizeRange.Max);
        float lifetime = Random.Range(spawnLifetime.Min, spawnLifetime.Max);

        float score = 1 - ((alpha - spawnAlpha.Min) / (spawnAlpha.Max - spawnAlpha.Min));
        score += 1 - ((rotSpeed - spawnRotSpeed.Min) / (spawnRotSpeed.Max - spawnRotSpeed.Min));
        score += 1 - ((major - spawnSizeRange.Min) / (spawnSizeRange.Max - spawnSizeRange.Min));
        score += 1 - ((minor - spawnSizeRange.Min) / (spawnSizeRange.Max - spawnSizeRange.Min));
        score += 1 - ((lifetime - spawnLifetime.Min) / (spawnLifetime.Max - spawnLifetime.Min));
        score *= bonusScoreAdjust;

        anomally.name = "anomally" + score.ToString("0.000");
        anomally.transform.parent = anomaliesHolder;
        anomally.SetActive(true);

        anomallyField.SetMagController(this);
        rotSpeed *= Random.value > 0.5f ? 1.0f : -1.0f;
        anomallyField.ChangeRotateSpeed(rotSpeed);
        anomallyField.Focus = Vector3.zero;
        anomallyField.Center = Random.insideUnitCircle.normalized * ((LevelController.mainAsteroid.GetComponent<CircleCollider2D>().radius * LevelController.mainAsteroid.transform.localScale.x) + major / 4.0f);
        if (major > minor)
        {
            anomallyField.MinorAxis = minor + orbitRadius / 2.0f;
            anomallyField.MajorAxis = major + pivotDistance / 2.0f;
        }
        else
        {
            anomallyField.MinorAxis = major + orbitRadius / 2.0f;
            anomallyField.MajorAxis = minor + pivotDistance / 2.0f;
        }

        anomallyField.Eccentricity = 0.0f;
        anomallyField.isAnomally = true;
        anomallyField.fieldScoreMod = score;
        anomallyField.lifeSpan = lifetime;
        anomallyField.alpha = alpha;
        anomallyField.angle2sun = GameObject.FindGameObjectWithTag("Sun").transform.position.normalized;
        anomallyField.InitializeFlux();
//        anomallyField.ChangeAlpha(0.0f);

        FitEllipse(anomallyField);
    }

    private void CreateMainFields()
    {
        GameObject main = Instantiate(fieldObject);
        GameObject frontField = Instantiate(fieldObject);
        GameObject backField = Instantiate(fieldObject);

        MagneticField mainField = main.GetComponent<MagneticField>();
        MagneticField sideFields = frontField.GetComponent<MagneticField>();


        main.name = "MainField";
        main.transform.parent = mainFieldsHolder;
        main.SetActive(true);
        
        mainField.SetMagController(this);
        mainField.ChangeRotateSpeed(2f);
        mainField.Focus = Vector3.zero; // zero is the position of the asteroid body
        mainField.Center = -GameObject.FindGameObjectWithTag("Sun").transform.position.normalized * pivotDistance; //move the center away some pivot distance opposite from the sun
        mainField.Eccentricity = orbitRadius;
        mainField.isAnomally = false;
        mainField.fieldScoreMod = 0.0f;

        DefineEllipse(mainField);
        mainField.InitializeFlux();
        mainField.ChangeAlpha(0.2f);
        FitEllipse(mainField);

        //FrontField===========
        frontField.name = "FrontField";
        frontField.transform.parent = mainFieldsHolder;
        frontField.SetActive(true);

        sideFields.SetMagController(this);
        sideFields.ChangeRotateSpeed(0.5f);
        sideFields.Focus = Vector3.zero;
        sideFields.Center = GameObject.FindGameObjectWithTag("Sun").transform.position.normalized * (mainField.Eccentricity * 0.4f); //move towards sun
        //sideFields.Focus = sideFields.Center + Vector3.Cross(GameObject.FindGameObjectWithTag("Sun").transform.position, Vector3.forward).normalized * mainField.Eccentricity * 0.4f;
        //sideFields.Center = (mainField.Focus - mainField.Center).normalized * (mainField.Eccentricity * 0.4f);
        sideFields.MinorAxis = mainField.MinorAxis * 0.6f;
        sideFields.MajorAxis = mainField.Eccentricity * 0.85f;
        sideFields.Eccentricity = mainField.Eccentricity;
        sideFields.isAnomally = false;
        sideFields.fieldScoreMod = 0.0f;

        //DefineEllipse(sideFields);
        sideFields.InitializeFlux();
        sideFields.ChangeAlpha(0.3f);

        FitEllipse(sideFields);

        //BackField==============
        backField.name = "BackField";
        backField.transform.parent = mainFieldsHolder;
        backField.SetActive(true);

        sideFields = backField.GetComponent<MagneticField>();
        sideFields.SetMagController(this);
        sideFields.ChangeRotateSpeed(-1f);
        sideFields.Focus = -GameObject.FindGameObjectWithTag("Sun").transform.position.normalized * (mainField.Eccentricity * 0.4f);
        //sideFields.Center = -GameObject.FindGameObjectWithTag("Sun").transform.position.normalized * (((mainField.MajorAxis - 2 * mainField.Eccentricity) * 0.4f) / 2.0f);
        sideFields.Center = mainField.Center + mainField.Center.normalized * (mainField.Eccentricity * 0.5f);
        //sideFields.MinorAxis = mainField.MinorAxis * 0.8f;
        //sideFields.MajorAxis = (mainField.Focus - mainField.Center).magnitude * 2.0f;
        sideFields.Eccentricity = mainField.Eccentricity * 0.4f;
        sideFields.isAnomally = false;
        sideFields.fieldScoreMod = 0.0f;

        DefineEllipse(sideFields);
        sideFields.InitializeFlux();
        sideFields.ChangeAlpha(0.3f);
        sideFields.MinorAxis += mainField.MinorAxis * 0.1f;
        FitEllipse(sideFields);
    }

    public void SetRingStatus(bool val)
    {
        inField = val;
    }

    private void FitEllipse(MagneticField field)
    {
        //scaling defined by size we want divided by the size we have
        float angle = Vector3.SignedAngle(Vector3.right, field.Focus - field.Center, Vector3.forward);
        Debug.Log("angle: " + angle);

        field.transform.position = field.Center;
        field.transform.localScale = new Vector3(field.MinorAxis, 1, field.MajorAxis);
        field.transform.Rotate(Vector3.up , angle);
    }

    private void DefineEllipse(MagneticField field)
    {
        field.MajorAxis = ((field.Focus - field.Center).magnitude + field.Eccentricity) * 2.0f;

        if (field.Center != field.Focus)
        {
            field.MinorAxis = (Mathf.Sqrt(field.Eccentricity * ((2 * (field.Focus - field.Center).magnitude) + field.Eccentricity))) * 2.0f;
        }
        else
        {
            field.MinorAxis = field.MajorAxis;
        }
    }

    public Vector3 GetRingCenter()
    {
        return mainFieldsHolder.Find("MainField").GetComponent<MagneticField>().Center;
    }

    public float GetRingSize()
    {
        return mainFieldsHolder.Find("MainField").GetComponent<MagneticField>().MajorAxis * 0.4f;
    }
    public void EndOfLevel()
    {
        Destroy(mainFieldsHolder.gameObject);
        Destroy(anomaliesHolder.gameObject);
    }

}
