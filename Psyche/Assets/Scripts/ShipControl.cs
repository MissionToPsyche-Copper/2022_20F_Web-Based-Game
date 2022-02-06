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


    // Start is called before the first frame update
    void Start()
    {
        shipAudioListener = this.GetComponent<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
        //||  Input.GetKey(KeyCode.W)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * Constants.Ship.Thrust);
            thrustTrail.SetActive(true);
        }
        else
            thrustTrail.SetActive(false);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(hardMode)
                this.GetComponent<Rigidbody2D>().angularVelocity += Constants.Ship.RotateSpeed;
            else
                this.transform.Rotate(Vector3.forward, Constants.Ship.RotateSpeed);
        }


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if(hardMode)
               this.GetComponent<Rigidbody2D>().angularVelocity += -Constants.Ship.RotateSpeed;
            else
               this.transform.Rotate(Vector3.forward, -Constants.Ship.RotateSpeed);
        }
    }
}
