using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    [Header("----- Flight Controls -----")]
    public float moveSpeed = 0.01f;
    public float rotateSpeed = 0.01f;
    public bool hardMode = false;

    [Header("----- Ship Parts -----")]
    public GameObject thrustTrail;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //||  Input.GetKey(KeyCode.W)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * Constants.shipThrust);
            thrustTrail.SetActive(true);
        }
        else
            thrustTrail.SetActive(false);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(hardMode)
                this.GetComponent<Rigidbody2D>().angularVelocity += Constants.shipRotateSpeed;
            else
                this.transform.Rotate(Vector3.forward, Constants.shipRotateSpeed);
        }


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if(hardMode)
               this.GetComponent<Rigidbody2D>().angularVelocity += -Constants.shipRotateSpeed;
            else
               this.transform.Rotate(Vector3.forward, -Constants.shipRotateSpeed);
        }
    }
}
