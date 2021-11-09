using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A game wide list of constants
/// this class of objects creates an easy to navigate and modify list of parameters affecting the whole game
/// 
/// the purpose is so that tweaks can easily be made here instead of having to navigate to each and every script
/// to modify different values for later game balancing.
/// </summary>
public class Constants : MonoBehaviour
{
    /// <summary>
    ///  Ship Properties
    /// </summary>
    public static class Ship
    {
        public const float Thrust = 2.5f;
        public const float RotateSpeed = 1.0f;
    }

    /// <summary>
    ///  Space Properties  
    /// </summary>
    public static class Space
    {
        public static float gravityConstant = 6.6743f * Mathf.Pow(10, -2.5f);
        public const float objRotateSpeed = 0.1f;
    }

    public static class Magnetometer
    {

    }

    public static class Imager
    {

    }

    public static class Radio
    {

    }

    public static class DSOC
    {

    }

    /// <summary>
    ///  Gamma Ray and Neutron Spectrometer Properties
    /// </summary>
    public static class Spectrometer
    {
        public static class Rotation
        {
            public const float MaxTimeChange = 5.0f;
            public const float MaxSpeed = 1.0f;
        }

        public static class Ray
        {
            public const float ejectMinTime = 2.0f;
            public const float ejectMaxTime = 10.0f;
            public const float ejectForce = 100.0f;
        }

        public static class Neutron
        {
            public const int emitMax = 4;  // the max random range of neutrons emitted upon ray impact
            public const float SelfDestTime = 10.0f;  //the life span of time for a neutron to exist
        }

        public static class Sounds
        {
            public const string PickupSoundFX = "/Audio/GammaRay/polygon_explosion_bullet.wav";
            public const string RayShootFX = "/Audio/GammaRay/polygon_shoot_lightning.wav";
            public const string RayCollideFX = "/Audio/GammaRay/polygon_explosion_shockwave.wav";
        }

    }
}


