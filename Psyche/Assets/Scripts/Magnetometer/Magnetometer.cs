using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Magnetometer : MonoBehaviour
{
    [Range(0,3)]
    public int RingSelect = 0;
    public GameObject[] ring;
    public GameObject target;
    public GameObject effect;


    [Range(0, 200)]
    public int pivotDistance = 60;
    [Range(30, 200)]
    public float orbitRadius; //periapsis distance also called r1
    private float a; //semi-major axis
    private float b; //semi-minor axis
    private Vector2 pivot;
    private Vector2 aDir;
    private Vector2 bDir;
    private float majorSize = 0.0f;

    [SerializeField] private float scoreRampUpTime = 5.0f;
    private float currRampTime = 0.0f;
    [SerializeField] private float scoreModAtRamp = 1.0f;

    [ReadOnly]
    [SerializeField] private bool inRing = false;
    public static bool toolActive = false;


    // Start is called before the first frame update
    void Start()
    {
        target.transform.position = GameRoot.player.transform.position;
        target.transform.parent = GameRoot.player.transform;
        for(int i = 0; i < ring.Length; i++)
        {
            ring[i].SetActive(false);
        }


        ring[RingSelect].SetActive(true);
        ring[RingSelect].transform.Find("inner").GetComponent<MagnetTrigger>().SetMagController(this);
        ring[RingSelect].transform.Find("outer").GetComponent<MagnetTrigger>().SetMagController(this);

        DefineEllipse();
        FitRingToEllipse();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(inRing)
        {
            if (ShipControl.resources.CanUsePower())
            {
                if(currRampTime < scoreRampUpTime)
                    currRampTime += Time.deltaTime;
                GameRoot._Root.ScoreMagnetometer(Time.deltaTime * (scoreModAtRamp * (currRampTime / scoreRampUpTime)));
                ShipControl.resources.UsePower(Constants.Ship.Resources.PowerUse.Magnetometer * Time.deltaTime);
            }
        }
    }

    public void SetRingStatus(bool val)
    {
        inRing = val;
        currRampTime = 0.0f;

        effect.SetActive(inRing);
    }

    private void FitRingToEllipse()
    {
        float ringInitRadius = 0.0f;
        switch (RingSelect)
        {
            case 0: ringInitRadius = 5.0f; break;
            case 1: ringInitRadius = 17.5f; break;
            case 2: ringInitRadius = 40.0f; break;
            case 3: ringInitRadius = 42.5f; break;
        }
        //scaling defined by size we want divided by the size we have

        float xScale = a / ringInitRadius;
        float yScale = b / ringInitRadius;
        Debug.Log("xScale: " + xScale + ",\t yScale: " + yScale);

        float angle = Vector3.SignedAngle(Vector3.right, aDir, Vector3.forward);
        Debug.Log("angle: " + angle);

        ring[RingSelect].transform.Rotate(Vector3.up, angle);
        ring[RingSelect].transform.localScale = new Vector3(yScale, 1, xScale);
        ring[RingSelect].transform.position = pivot;

    }

    private void DefineEllipse()
    {
        //pivot defines our (h,k)
        if(pivotDistance != 0)
        {
            pivot = Random.insideUnitCircle.normalized * pivotDistance;
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

    public Vector3 GetRingCenter()
    {
        return ring[RingSelect].transform.position;
    }

    public float GetRingSize()
    {
        return a;
    }

}
