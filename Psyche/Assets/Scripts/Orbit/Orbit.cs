using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Orbit : MonoBehaviour
{
    [Header("========= Controls ===========")]
    [Tooltip("This is the IN GAME altitude")]
    [Range(0, 300)]
    [SerializeField] private float pivotDistance = 60;
    [Tooltip("This vector will be normalized")]
    [SerializeField] private Vector3 pivotDirection = Vector3.right;
    [Tooltip("This is the IN GAME altitude")]
    [Range(30, 800)]
    [SerializeField] private float orbitRadius; //periapsis distance also called r1
    [Range(5, 50)]
    [SerializeField] private float orbitWidth; 

    [Header("========= Objects =============")]
    [SerializeField] private LineRenderer line;
    [SerializeField] private GameObject target;
    [SerializeField] private Transform Triggers;
    [SerializeField] private Transform trig_Out_transform;
    [SerializeField] private Transform trig_In_transform;
    [SerializeField] private OrbitTrigger trigger_out;
    [SerializeField] private OrbitTrigger trigger_in;



    [Header("========= Score Bonus =============")]
    [SerializeField] private float scoreRampUpTime = 90.0f;
    private static float currRampTime = 0.0f;
    [SerializeField] private float scoreModAtRamp = 4.0f;

    [Header("========= PopUp Hints =============")]

    public string[] hints = new string[] { "Match your orbit to the blue orbit ring to maximize your score", "You earn more points when you're aligned to the mission orbit" };
    public float hintTimeMax = 30.0f;
    private float hintTime = 0.0f;

    //Orbit Information
    private float a; //semi-major axis
    private float b; //semi-minor axis
    private Vector2 pivot;
    private Vector2 aDir;
    private Vector2 bDir;
    private float theta;
    private float majorSize = 0.0f;

    [ReadOnly]
    [SerializeField] private static bool inRing = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Init()
    {
        pivotDistance /= Constants.Space.AltitudeFactorAdjust;
        orbitRadius /= Constants.Space.AltitudeFactorAdjust;
        DefineEllipse();
        DrawEllipse();
        FitTriggers();
        FitRingToEllipse();
        target.transform.position = LevelController.player.transform.position;
        target.transform.parent = LevelController.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(inRing)
        {
            hintTime = 0.0f;
            if(currRampTime < scoreRampUpTime)
                currRampTime += Time.deltaTime;
            LevelController.levelRoot.SetBonus(1.0f + (scoreModAtRamp * (currRampTime / scoreRampUpTime)));
        }
        else
        {
            if (currRampTime > 0.0f)
                currRampTime -= Time.deltaTime * 5.0f;
            LevelController.levelRoot.SetBonus(1.0f + (scoreModAtRamp * (currRampTime / scoreRampUpTime)));


            hintTime += Time.deltaTime;
            if(hintTime > hintTimeMax)
            {
                PopMessageUI.PopUpMessage(hints[Random.Range(0, hints.Length)]);
                hintTime = 0.0f;
            }
        }
    }

    private void DefineEllipse()
    {
        //pivot defines our (h,k)
        if (pivotDistance != 0)
        {
            pivot = pivotDirection.normalized * pivotDistance;
            a = pivot.magnitude + orbitRadius;
            aDir = pivot.normalized;
            float r2 = a + pivot.magnitude;
            b = Mathf.Sqrt(orbitRadius * r2); //orbitdistance = r1, and sqrt(r1 * r2) = b
            Debug.Log("a: " + a + ",\t b: " + b);
            bDir = Vector3.Cross(new Vector3(aDir.x, aDir.y, 0), Vector3.forward).normalized;
        }
        else
        {
            pivot = Vector2.zero;
            a = orbitRadius;
            b = orbitRadius;
            aDir = Vector2.right;
            bDir = Vector2.up;
        }

        Vector2 semiMajorAxis = aDir * a;
        if (Mathf.Abs(semiMajorAxis.x) >= Mathf.Abs(semiMajorAxis.y))
            majorSize = semiMajorAxis.x;
        else
            majorSize = semiMajorAxis.y;
    }


    private void DrawEllipse()
    {
        line.positionCount = 0;
        List<Vector3> points = new List<Vector3>();
        float x, y;
        theta = Vector3.SignedAngle(Vector3.right, pivot.normalized, Vector3.forward) * Mathf.Deg2Rad;
        for(float i = 0; i < 1.99; i+=0.01f)
        {
            line.positionCount++;
            //initial x/y position in space
            x = a * Mathf.Cos(i * Mathf.PI);
            y = b * Mathf.Sin(i * Mathf.PI);

            //rotated position of orbit
            Vector2 temp = new Vector2(x, y);
            x = temp.x * Mathf.Cos(theta) - temp.y * Mathf.Sin(theta);
            y = temp.x * Mathf.Sin(theta) + temp.y * Mathf.Cos(theta);

            //translation of point
            x += pivot.x;
            y += pivot.y;

            line.SetPosition(line.positionCount - 1, new Vector3(x, y, 5));
        }
        line.startWidth = orbitWidth;
        line.endWidth = orbitWidth;
    }

    public void FitTriggers()
    {
        trigger_in.insideRing = true;
        trigger_out.insideRing = false;

        //Inner Ring
        trig_In_transform.localScale = new Vector3((a * 2) - (orbitWidth), (b * 2) - (orbitWidth), 1);
        trig_Out_transform.localScale = new Vector3((a * 2) + (orbitWidth), (b * 2) + (orbitWidth), 1);
        Triggers.Rotate(Vector3.forward, theta * Mathf.Rad2Deg);
        Triggers.position = pivot;
    }

    public static void SetOrbitStatus(bool val)
    {
        inRing = val;
        //currRampTime = 0.0f;
        //if (!inRing)
        //    LevelController.levelRoot.SetBonus(1.0f);
    }

    private void FitRingToEllipse()
    {
        float ringInitRadius = 0.0f;
        //switch (RingSelect)
        //{
        //    case 0: ringInitRadius = 5.0f; break;
        //    case 1: ringInitRadius = 17.5f; break;
        //    case 2: ringInitRadius = 40.0f; break;
        //    case 3: ringInitRadius = 42.5f; break;
        //}
        //scaling defined by size we want divided by the size we have

        float xScale = a / ringInitRadius;
        float yScale = b / ringInitRadius;
        Debug.Log("xScale: " + xScale + ",\t yScale: " + yScale);

        float angle = Vector3.SignedAngle(Vector3.right, aDir, Vector3.forward);
        Debug.Log("angle: " + angle);

        //ring[RingSelect].transform.Rotate(Vector3.up, angle);
        //ring[RingSelect].transform.localScale = new Vector3(yScale, 1, xScale);
        //ring[RingSelect].transform.position = pivot;

    }


    public Vector3 GetOrbitCenter()
    {
        return pivotDirection.normalized * (pivotDistance / Constants.Space.AltitudeFactorAdjust);
    }

    public float GetOrbitSemiMajor()
    {
        return a;
    }

}
