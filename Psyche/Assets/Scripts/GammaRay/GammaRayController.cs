using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTimer;

public class GammaRayController : MonoBehaviour
{
    public ObjectRotate pivotPoint;
    private Timer pivotTimer;
    

    // Start is called before the first frame update
    void Start()
    {
        pivotTimer = Timer.Register(Random.Range(1f, Constants.pivotMaxTime), this.ChangePivotSpeed, isLooped: false, useRealTime: false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangePivotSpeed()
    {
        Timer.Cancel(pivotTimer);
        pivotPoint.SetSpeed(Random.Range(0.0001f, Constants.pivotMaxSpeed));
        pivotTimer = Timer.Register(Random.Range(1f, Constants.pivotMaxTime), this.ChangePivotSpeed, isLooped: false, useRealTime: false);

    }
}
