using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class MagneticField : MonoBehaviour
{
    //Construction Details
    public Vector3 Focus { get; set; }
    public Vector3 Center { get; set; }
    public float Eccentricity { get; set; }
    public float MajorAxis { get; set; }
    public float MinorAxis { get; set; }

    //GameMechanic Details
    public bool isAnomally { private get; set; }
    public float lifeSpan { private get; set; }
    public float fieldScoreMod { private get; set; }
    public float alpha { private get; set; }
    public Vector3 angle2sun { private get; set; }

    private float currLife = 0.0f;
    

    private MagnetometerController magController;
    public SpriteRenderer sprite;
    public ObjectRotate rotation;
    public GameObject fluxLineMaster;
    private List<TrailRenderer> fluxLines;
    private float messgWait = 0.0f;
    private float messgMax = 2.0f;

    public void SetMagController(MagnetometerController control)
    {
        magController = control;
    }

    public void ChangeAlpha(float val)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.b, sprite.color.b, val);
        foreach (TrailRenderer line in fluxLines)
            line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, val);
    }

    public void ChangeRotateSpeed(float val)
    {
        rotation.SetSpeed(val);
    }

    public Vector3 GetFieldCenter()
    {
        return this.transform.position;
    }

    public void FixedUpdate()
    {
        if (!isAnomally)
            return;
        else
        {
            currLife += Time.deltaTime;
            if (currLife < lifeSpan * 0.125f) //fadein
            {
                float modalpha = alpha * (currLife / (lifeSpan * 0.125f));
                ChangeAlpha(modalpha);
            }

            if (currLife > lifeSpan * 0.75f) //fadeout
            {
                float modalpha = alpha * (1 - (currLife - lifeSpan * 0.75f) / (lifeSpan - lifeSpan * 0.75f));
                ChangeAlpha(modalpha);
            }

            float posVsSunAngle = Vector3.Angle(angle2sun, this.transform.position.normalized);
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, MajorAxis * 0.5f + Mathf.Pow(MajorAxis * 4, (posVsSunAngle / 180.0f) * 0.9f ));
            this.transform.position = this.transform.position.normalized * ((GameRoot.mainAsteroid.GetComponent<CircleCollider2D>().radius * GameRoot.mainAsteroid.transform.localScale.x) - (1 - Mathf.Pow(MajorAxis * 1.5f, (posVsSunAngle / 180.0f) * 0.9f)));


            if (currLife > lifeSpan)
            {
                magController.SetRingStatus(false);
                magController.bonusScoreMod -= fieldScoreMod;
                if (magController.bonusScoreMod < 0.0f)
                    magController.bonusScoreMod = 0.0f;

                Destroy(this.gameObject);
            }
        }

        //if (messgWait > messgMax)
        //    return;
        //messgWait += Time.deltaTime;
    }


    public void OnTriggerStay(Collider other)
    {
        magController.SetRingStatus(true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "magnet")
        {
            magController.bonusScoreMod += fieldScoreMod;
            //if (messgWait >= messgMax)
            //{
            //    Magnetometer.toolActive = true;
            //    PopMessageUI.PopUpMessage("Entering Magnetometer Orbit", 4.0f);
            //    messgWait = 0.0f;
            //}

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "magnet")
        {
            magController.SetRingStatus(false);
            magController.bonusScoreMod -= fieldScoreMod;
            if (magController.bonusScoreMod < 0.0f)
                magController.bonusScoreMod = 0.0f;
            //if (messgWait >= messgMax)
            //{
            //    Magnetometer.toolActive = false;
            //    PopMessageUI.PopUpMessage("Exiting Magnetometer Orbit", 4.0f);
            //    messgWait = 0.0f;
            //}

        }
    }

    public void InitializeFlux()
    {
        //how big is is this ring to start?
        //doesn't have to be perfect just need something to scale with
        //so I'm taking the Area of a circle to use
        float fluxArea = Mathf.PI * Mathf.Pow(MajorAxis / 2.0f, 2.0f); //outside ring
        
        Debug.Log(this.gameObject.name + " FluxArea: " + fluxArea.ToString("0.0000"));
        Vector2 dir;
        GameObject fluxLine;
        TrailRenderer fluxTrail;
        fluxLines = new List<TrailRenderer>();
        int sizeMod = Mathf.CeilToInt( fluxArea / 1000.0f );
        Debug.Log(this.gameObject.name +  " SizeMod: " + sizeMod.ToString());
        float timeSizeMod = Mathf.Pow(2, MinorAxis / MajorAxis); //bigger size means longer lasting
        float timeRotMod = 1.0f / Mathf.Abs(rotation.rotationSpeed); //faster rotation means shorter lasting
        float timeBaseMax = 0.5f;
        float timeModMulti = 1.2f;
        float maxTime = timeBaseMax + (timeModMulti * timeRotMod * timeSizeMod);
        Debug.Log(this.gameObject.name + " MaxTime: " + maxTime.ToString());


        for (int i = 0; i < 10 + sizeMod; i++)
        {
            fluxLine = Instantiate(fluxLineMaster);
            fluxLine.SetActive(true);
            fluxLine.transform.parent = sprite.transform;

            dir = Random.insideUnitCircle.normalized;
            fluxLine.transform.localPosition = dir * Random.Range(0.85f, 1.0f) * 2;

            fluxTrail = fluxLine.GetComponent<TrailRenderer>();
            if (isAnomally)
            {
                int val = Random.Range(0,3);
                switch(val)
                {
                    case 0:
                        fluxTrail.startColor = new Color(Random.Range(0.8f, 1.0f), 0, 0, 1);
                        fluxTrail.endColor = new Color(Random.Range(0.2f, 0.5f), 0, 0, 0);
                        break;
                    case 1:
                        fluxTrail.startColor = new Color(0, Random.Range(0.8f, 1.0f), 0, 1);
                        fluxTrail.endColor = new Color(0, Random.Range(0.2f, 0.5f), 0, 0);
                        break;
                    case 2:
                        fluxTrail.startColor = new Color(0, 0, Random.Range(0.8f, 1.0f), 1);
                        fluxTrail.endColor = new Color(0, 0, Random.Range(0.2f, 0.5f), 0);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                fluxTrail.startColor = new Color(0, 1, 0, 1);
                fluxTrail.endColor = new Color(0, Random.Range(0.5f, 1.0f), 0, 0);

            }

            fluxTrail.startWidth = Random.Range(1.5f, 5.0f);
            fluxTrail.time = Random.Range(0.1f, timeBaseMax + (timeModMulti * timeRotMod * timeSizeMod));

            fluxLines.Add(fluxTrail);

        }
    }
}
