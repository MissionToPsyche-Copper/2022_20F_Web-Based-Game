using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    //--------------------
    //  Ship Properties
    //--------------------
    public const float shipThrust = 2.0f;
    public const float shipRotateSpeed = 0.75f;

    //--------------------
    //  Space Properties
    //--------------------
    public static float gravityConstant = 6.6743f * Mathf.Pow(10, -2.5f);
    public static float objRotateSpeed = 0.1f;

    //--------------------
    //  GammaRay/Neutron Properties
    //--------------------
    public static float pivotMaxTime = 5.0f;
    public static float pivotMaxSpeed = 1.0f;
    public static float ejectMinTime = 2.0f;
    public static float ejectMaxTime = 10.0f;
    public static float ejectForce = 100.0f;
    public static int emitMaxNeutrons = 4;
    public static float neutronSelfDestTime = 10.0f;


}
