using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class MultiSpectController : MonoBehaviour
{
    public static MultiSpectController instance;

    private int targetCount;

    [Header("------Spawning Target Controls--------")]
    [MinMaxRange(0,100, order = 1)]
    [Tooltip("Chance to spawn a new surface anomally.\n")]
    public RangedInt spawnChance = new RangedInt(50,75);
    private int spawnVal;
    [MinMaxRange(0.0f, 20.0f, order = 2)]
    [Tooltip("The time interval (in seconds) between each spawn window")]
    public RangedFloat spawnInterval = new RangedFloat(3.0f, 7.0f);
    private float intervalVal;
    [Range(0.0f, 10.0f)]
    [Tooltip("An offset amount used to tweak how far off the surface of the asteroid every anomally is positioned")]
    public float spawnOffset;
    [Range(0.0f, 20.0f)]
    [Tooltip("Sets the minimum distance or spacing targets can be from eachother")]
    public float distBetweenTargets = 10.0f;
    [MinMaxRange(1.0f, 10.0f)]
    [Tooltip("Adjusts the ranged of sizes that target anomallies will appear to be.\nThe size of the anomally also plays into how much points they are worth when scanned (smaller size = more points)")]
    public RangedFloat spawnSizeRange = new RangedFloat(3.0f, 6.0f);
    [MinMaxRange(0.0f, 20.0f, order = 2)]
    [Tooltip("How long an anomally will appear before disappearing")]
    public RangedFloat spawnLifetime = new RangedFloat(3.0f, 7.0f);
    private float currentTime = 0;

    [Header("------ScanLine Controls--------")]
    [SerializeField]
    [Range(0.0f, 180.0f)]
    private float maxTargetAngle = 60.0f;
    [Range(0.1f, 10.0f)]
    public float lineEndWidth;
    public Color lineClrEnd;
    public Color lineClrStart;
    public Color offAngleColor;

    [Header("------Audio Controls--------")]
    [MinMaxRange(0.0f, 10.0f)]
    public RangedFloat pitchShiftRange = new RangedFloat(2.0f, 6.0f);
    [SerializeField][ReadOnly]
    private float currPitch = 3.0f;

    [Header("------Misc Controls--------")]
    [Range(0.0f, 20.0f)]
    public float scoreMod; 
    public bool easyMode;

    [Header("------Components--------")]    
    [Foldout("Components", true)]
    [SerializeField]
    private GameObject pivot;
    [SerializeField]
    private GameObject Target;
    private List<GameObject> targetList;
    [SerializeField]
    private GameObject shipAntenna;
    private LineRenderer scanLine;
    private GameObject antennaPoint;
    private CircleCollider2D asteroid;
    private AudioSource audioEmitter;

	public static bool toolActive = false;

    void Start()
    {
        instance = this;    

        asteroid = SceneController.mainAsteroid.GetComponent<CircleCollider2D>();
        targetList = new List<GameObject>();
        pivot.GetComponent<ObjectRotate>().SetAxis(asteroid.gameObject.GetComponent<ObjectRotate>().rotationAxis);
        intervalVal = Random.Range(spawnInterval.Min, spawnInterval.Max);
        scanLine = this.GetComponent<LineRenderer>();
        audioEmitter = shipAntenna.GetComponent<AudioSource>();
        audioEmitter.volume = Constants.Multispectral.Sounds.beamVolume * Constants.Audio.masterVolume;
        antennaPoint = shipAntenna.transform.Find("AntennaPoint").gameObject;
        shipAntenna.transform.position = SceneController.player.transform.position;
        shipAntenna.transform.parent = SceneController.player.transform;
    }

    private Vector3 GenerateTargetPosition(int iteration)
    {
        if (iteration > 20)
            return Vector3.zero;

        float dist2AnotherTarget = 1000.0f;
        Vector3 circleDirection = Random.insideUnitCircle.normalized;
        circleDirection = circleDirection.normalized * asteroid.radius * (asteroid.gameObject.transform.localScale.x) + (circleDirection.normalized * spawnOffset);
        circleDirection.z = -1;

        //Check that this target is not too close to any other active targets
        for(int i = 0; i < targetList.Count; i++)
        {
            dist2AnotherTarget = (circleDirection - targetList[i].transform.position).magnitude;

            if (dist2AnotherTarget < distBetweenTargets)
                return GenerateTargetPosition(iteration++);
        }

        return circleDirection;
    }

    private Quaternion GenerateTargetInitDirection(Vector3 pos)
    {
        Vector3 upDirect = pos.normalized;

        Quaternion quatRotate = Quaternion.LookRotation(Vector3.forward, upDirect);
        
        return quatRotate;
    }

    public void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > intervalVal)
        {
            spawnVal = Random.Range(spawnChance.Min, spawnChance.Max);
            if (Random.value * 100.0f <= spawnVal)
            {
                GameObject temp = Instantiate(Target);
                float spawnSize = Random.Range(spawnSizeRange.Min, spawnSizeRange.Max);
                temp.transform.localScale *= spawnSize;
                temp.transform.parent = pivot.transform;
                temp.transform.position = GenerateTargetPosition(0);
                temp.transform.rotation = GenerateTargetInitDirection(temp.transform.position);
                temp.GetComponent<Target>().SetScoreSizeMod(spawnSizeRange.Min / spawnSize);
                temp.GetComponent<Target>().SetLifespan(Random.Range(spawnLifetime.Min, spawnLifetime.Max));
                temp.GetComponent<Target>().SetEZmode(easyMode);
                targetList.Add(temp);
                temp.GetComponent<Target>().SetController(this);
            }
            intervalVal = Random.Range(spawnInterval.Min, spawnInterval.Max);
            currentTime = 0;
        }
    }

    public AudioSource GetAudio()
    {
        return audioEmitter;
    }

    public void RemoveFromTargetList(GameObject target)
    {
        targetList.Remove(target);
    }

    public Vector3 GetAntennaPos()
    {
        return antennaPoint.transform.position;
    }

    public LineRenderer GetLine()
    {
        return scanLine;
    }

    public void ToggleLine(bool val)
    {
        scanLine.enabled = val;
    }


    /// <summary>
    /// Sets the two ends of the line from the shipAntenna position
    /// to the target anomally position;
    /// </summary>
    public void SetLinePoints(Vector3 targetPos)
    {
        scanLine.SetPosition(0, antennaPoint.transform.position);
        scanLine.SetPosition(1, new Vector3(targetPos.x, targetPos.y, 0.0f));
    }

    /// <summary>
    /// Shift the width of the end of the scanLine depending on what angle
    /// the ship is facing vs the position of the target
    /// </summary>
    /// <param name="val">percentage of current ship direction vs the maximum angle</param>
    public void SetLineWidth(float val)
    {
        scanLine.endWidth = lineEndWidth * (1.0f - val);
    }

    /// <summary>
    /// Shifts the color of the line from good to bad depending on angle to target
    /// as the player shifts off angle the base color decreases and the offAngleColor increases
    /// </summary>
    /// <param name="val">percentage of current ship direction vs the maximum angle</param>
    public void SetLineColors(float val)
    {
        Color tempEnd = ((lineClrEnd * (1.0f - val)) + (offAngleColor * val)) / 2.0f;
        Color tempStart = ((lineClrStart * (1.0f - val)) + (offAngleColor * val)) / 2.0f;
        scanLine.endColor = new Color(tempEnd.r, tempEnd.g, tempEnd.b, 0.85f);
        scanLine.startColor = new Color(tempStart.r, tempStart.g, tempStart.b, 1.0f);
    }

    public void SetAudioPitch(float val)
    {
        currPitch = ((pitchShiftRange.Max * (1 - val)) - pitchShiftRange.Min) + pitchShiftRange.Min;
        audioEmitter.pitch = currPitch;
    }

    /// <summary>
    /// Add score to GameRoot depending on how direct the scan is, 
    /// what the scoreMod is and time on target.
    /// </summary>
    /// <param name="anglePercent">percentage of current ship direction vs the maximum angle</param>
    public void AddScore(float anglePercent, float targSizeMod, float time)
    {
        SceneController.sceneRoot.ScoreMultispect(scoreMod * targSizeMod * time * (1.0f - anglePercent));
    }

}
