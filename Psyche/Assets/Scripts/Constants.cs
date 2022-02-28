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
    public const float masterVolume = 1.0f;

    /// <summary>
    ///  Ship Properties
    /// </summary>
    public static class Ship
    {
        public const float Thrust = 3.0f;
        public const float RotateSpeed = 1.65f;

        public static class Resources
        {
            public const float MaxFuel = 500.0f;
            public const float BurnRatePS = 50.0f;
            public const float MaxPower = 500.0f;
            public const float PowerRechargePS = 40.0f;

            public static class PowerUse
            {
                public const float GyroRotate = 50.0f;
                public const float GammaRay = 100.0f;
                public const float Multispectral = 60.0f;
                public const float Magnetometer = 15.0f;
            }

        }

        public static class Sounds
        {
            //needs a soundFX for rotation
            //needs a soundFX for thrust
        }

    }

    /// <summary>
    ///  Space Properties  
    /// </summary>
    public static class Space
    {
        public static float gravityConstant = 6.6743f * Mathf.Pow(10, -2.5f);
        public const float objRotateSpeed = 0.3f;

        public static class Sounds
        {

        }
    }

    public static class Magnetometer
    {
        public static class Sounds
        {

        }
    }

    public static class Multispectral
    {
        public static class Sounds
        {
            public const float beamVolume = 0.1f; 
        }
    }

    public static class Radio
    {
        public static class Sounds
        {
            public const float ringVolume = 0.05f;
        }
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

        public static class TractorBeam
        {
            public const float BeamWidth = 20.0f;
            public const float BeamLength = 15.0f;
            public const int BeamSmooth = 11;
            public const float BeamStrength = -50.0f;
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
            public const float neutronEmitVolume = 0.0001f;
            public const float neutronCollectVolume = 0.5f;
            public const float tractorBeamVolume = 0.3f;

            public const string PickupSoundFX = "/Audio/GammaRay/polygon_explosion_bullet.wav";
            public const string RayShootFX = "/Audio/GammaRay/polygon_shoot_lightning.wav";
            public const string RayCollideFX = "/Audio/GammaRay/polygon_explosion_shockwave.wav";
            public const string TractorBeamFX = "Audio/GammaRay/Blastwave_FX_ArchWelderSpark_S08IN.52.wav";

            public const string pickupFX1 = "/Audio/GammaRay/zapsplat_multimedia_game_sound_collect_treasure_coin_001_40559.wav";
            public const string pickupFX2 = "/Audio/GammaRay/zapsplat_multimedia_game_sound_collect_treasure_coin_002_40560.wav";
            public const string pickupFX3 = "/Audio/GammaRay/zapsplat_multimedia_game_sound_collect_treasure_coin_003_40561.wav";
        }

        public static class Music
        {
            //gameplay tracks
            public const string BlazingStars = "/Audio/Music/Gameplay/Blazing-Stars_Looping.mp3";
            public const string ColdMoon = "/Audio/Music/Gameplay/Cold-Moon_Looping.mp3";
            public const string CyberREM = "/Audio/Music/Gameplay/Cyber-REM_Looping.mp3";
            public const string RetroSciFiPlanet = "/Audio/Music/Gameplay/Retro-Sci-Fi-Planet_Looping.mp3";
            public const string SciFiClose2 = "/Audio/Music/Gameplay/Sci-Fi-Close-2_Looping.mp3";
            public const string StarLight = "/Audio/Music/Gameplay/Star-Light_Looping.mp3";
            public const string TerraformingBegins = "/Audio/Music/Gameplay/Terraforming-Begins_Looping.mp3";
            public const string TroubleOnMercury = "/Audio/Music/Gameplay/Trouble-on-Mercury_Looping.mp3";

            //menu tracks
            public const string CosmicSwitchboard = "/Audio/Music/Menu/Cosmic-Switchboard.mp3";
            public const string CyberStreets = "/Audio/Music/Menu/Cyber-Streets.mp3";
            public const string DystopicTechnology = "/Audio/Music/Menu/Dystopic-Technology.mp3";
            public const string OminousSciFiMenu = "/Audio/Music/Menu/Ominous-Sci-Fi-Menu.mp3";
        }
    }
}


