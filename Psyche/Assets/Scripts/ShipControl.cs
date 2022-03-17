using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    [HideInInspector]
    public static AudioListener shipAudioListener;

    [Header("----- Flight Controls -----")]
    public bool hardMode = false;

    [Header("----- Ship Parts -----")]
    public GameObject thrustTrail;
    public static Transform shipAnt;
    public static ShipResources resources;
    public static bool gyroActive = false;
    public static bool isThrusting = false;

    public void Awake()
    {
        shipAnt = this.transform.Find("ShipAnt");

    }

    // Start is called before the first frame update
    void Start()
    {
        shipAudioListener = this.GetComponent<AudioListener>();
        resources = this.gameObject.GetComponent<ShipResources>();
    }

    // Update is called once per frame
    void Update()
    {
        //||  Input.GetKey(KeyCode.W)
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && resources.CanUseFuel())
        {
            this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * Constants.Ship.Thrust);
            resources.UseFuel(Constants.Ship.Resources.BurnRatePS * Time.deltaTime);
            thrustTrail.SetActive(true);
            isThrusting = true;
        }
        else
        {
            thrustTrail.SetActive(false);
            isThrusting = false;
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && resources.CanUsePower())
        {
            if(hardMode)
                this.GetComponent<Rigidbody2D>().angularVelocity += Constants.Ship.RotateSpeed;
            else
                this.transform.Rotate(Vector3.forward, Constants.Ship.RotateSpeed);
            resources.UsePower(Constants.Ship.Resources.PowerUse.GyroRotate * Time.deltaTime);
        }


        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && resources.CanUsePower())
        {
            if(hardMode)
               this.GetComponent<Rigidbody2D>().angularVelocity += -Constants.Ship.RotateSpeed;
            else
               this.transform.Rotate(Vector3.forward, -Constants.Ship.RotateSpeed);
            resources.UsePower(Constants.Ship.Resources.PowerUse.GyroRotate * Time.deltaTime);
        }
    }
}
