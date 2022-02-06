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

    [HideInInspector]
    public static AudioClip neutronPickupFX;
    public static AudioClip rayShootFX;
    public static AudioClip rayCollideFX;

    private void Awake()
    {
        instance = this;

        //Controller Audio Pre-Loads
        neutronPickupFX = Resources.Load<AudioClip>(Constants.Spectrometer.Sounds.PickupSoundFX);
        rayShootFX = Resources.Load<AudioClip>(Constants.Spectrometer.Sounds.RayShootFX);
        rayCollideFX = Resources.Load<AudioClip>(Constants.Spectrometer.Sounds.RayCollideFX);
    }

    // Start is called before the first frame update
    void Start()
    {
        pivotTimer = Timer.Register(Random.Range(1f, Constants.Spectrometer.Rotation.MaxTimeChange), this.ChangePivotSpeed, isLooped: false, useRealTime: false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangePivotSpeed()
    {
        Timer.Cancel(pivotTimer);
        pivotPoint.SetSpeed(Random.Range(0.0001f, Constants.Spectrometer.Rotation.MaxSpeed));
        pivotTimer = Timer.Register(Random.Range(1f, Constants.Spectrometer.Rotation.MaxTimeChange), this.ChangePivotSpeed, isLooped: false, useRealTime: false);

    }
}
