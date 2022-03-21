using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTimer;

public class GammaRayController : MonoBehaviour
{
    public static GammaRayController instance;
    public ObjectRotate pivotPoint;
    private Timer pivotTimer;

    public GameObject raysList;
    public GameObject neutronsList;


    public AudioClip[] neutronPickupFX;
    public static AudioClip rayShootFX;
    public static AudioClip rayCollideFX;

    public GameObject collector;
    public LineRenderer TractorLine;
    public PolygonCollider2D TractorCollider;
    public PointEffector2D TractorEffector;
    public CircleCollider2D TractorCollector;
    private AudioSource tractorAudioFX;

	public static bool toolActive = false;

    private void Awake()
    {
        instance = this;

        //Controller Audio Pre-Loads
//        neutronPickupFX = Resources.Load<AudioClip>(Constants.Spectrometer.Sounds.PickupSoundFX);
        rayShootFX = Resources.Load<AudioClip>(Constants.Spectrometer.Sounds.RayShootFX);
        rayCollideFX = Resources.Load<AudioClip>(Constants.Spectrometer.Sounds.RayCollideFX);
//        neutronPickupFX = new List<AudioClip>();
        //neutronPickupFX.Add(Resources.Load<AudioClip>(Constants.Spectrometer.Sounds.pickupFX1));
        //neutronPickupFX.Add(Resources.Load<AudioClip>(Constants.Spectrometer.Sounds.pickupFX2));
        //neutronPickupFX.Add(Resources.Load<AudioClip>(Constants.Spectrometer.Sounds.pickupFX3));
    }

    // Start is called before the first frame update
    void Start()
    {
        pivotTimer = Timer.Register(Random.Range(1f, Constants.Spectrometer.Rotation.MaxTimeChange), this.ChangePivotSpeed, isLooped: false, useRealTime: false);

        tractorAudioFX = collector.GetComponent<AudioSource>();
        tractorAudioFX.volume = Constants.Spectrometer.Sounds.tractorBeamVolume * Constants.Audio.masterVolume;
        InitTractorBeam();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && ShipControl.resources.CanUsePower())
        {
            collector.SetActive(true);
            tractorAudioFX.Play();
            toolActive = true;
        }

        if(collector.activeInHierarchy)
        {
            if (!ShipControl.resources.CanUsePower())
            {
                tractorAudioFX.Stop();
                toolActive = false;
                collector.SetActive(false);
                return;
            }

            TractorLine.SetPosition(0, ShipControl.shipAnt.position);
            TractorLine.SetPosition(1, ShipControl.shipAnt.position + ShipControl.shipAnt.up * Constants.Spectrometer.TractorBeam.BeamLength);
            ShipControl.resources.UsePower(Constants.Ship.Resources.PowerUse.GammaRay * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(1))
        {
            collector.SetActive(false);
            tractorAudioFX.Stop();
            toolActive = false;
        }
    }


    private void ChangePivotSpeed()
    {
        Timer.Cancel(pivotTimer);
        pivotPoint.SetSpeed(Random.Range(0.0001f, Constants.Spectrometer.Rotation.MaxSpeed));
        pivotTimer = Timer.Register(Random.Range(1f, Constants.Spectrometer.Rotation.MaxTimeChange), this.ChangePivotSpeed, isLooped: false, useRealTime: false);

    }


    private void InitTractorBeam()
    {
        TractorLine.numCapVertices = Constants.Spectrometer.TractorBeam.BeamSmooth;
        TractorLine.endWidth = Constants.Spectrometer.TractorBeam.BeamWidth;
        TractorEffector.forceMagnitude = Constants.Spectrometer.TractorBeam.BeamStrength;

        Vector2[] tractorPath = new Vector2[Constants.Spectrometer.TractorBeam.BeamSmooth + 5];
        tractorPath[0] = Vector2.down;
        tractorPath[1] = Vector2.right;
        tractorPath[2] = new Vector2(Constants.Spectrometer.TractorBeam.BeamWidth / 2, Constants.Spectrometer.TractorBeam.BeamLength);

        float mag = Constants.Spectrometer.TractorBeam.BeamWidth / 2.0f;
        Vector2 temp = new Vector2(mag, 0);
        float theta = 0.0f;
        float thetaStep = 180.0f / Constants.Spectrometer.TractorBeam.BeamSmooth;
        thetaStep *= Mathf.Deg2Rad;

        for (int i = 3; i < tractorPath.Length - 2; i++)
        {
            theta += thetaStep;
            tractorPath[i] = Rotate(temp, theta);
            tractorPath[i].y += Constants.Spectrometer.TractorBeam.BeamLength;

        }

        tractorPath[tractorPath.Length - 2] = new Vector2(-Constants.Spectrometer.TractorBeam.BeamWidth / 2, Constants.Spectrometer.TractorBeam.BeamLength);
        tractorPath[tractorPath.Length - 1] = Vector2.left;

        TractorCollider.SetPath(0, tractorPath);

        collector.transform.position = ShipControl.shipAnt.position;

        collector.transform.parent = ShipControl.shipAnt;
        collector.SetActive(false);
    }

    private Vector2 Rotate(Vector2 vect, float theta)
    {
        return new Vector2(
            vect.x * Mathf.Cos(theta) - vect.y * Mathf.Sin(theta),
            vect.x * Mathf.Sin(theta) + vect.y * Mathf.Cos(theta)
            );
    }
}
