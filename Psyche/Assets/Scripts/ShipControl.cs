using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public float moveSpeed = 0.01f;
    public float rotateSpeed = 0.01f;
    public bool hardMode = false;

    
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
            this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * moveSpeed);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(hardMode)
                this.GetComponent<Rigidbody2D>().angularVelocity += rotateSpeed;
            else
                this.transform.Rotate(Vector3.forward, rotateSpeed);
        }


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if(hardMode)
               this.GetComponent<Rigidbody2D>().angularVelocity += -rotateSpeed;
            else
               this.transform.Rotate(Vector3.forward, -rotateSpeed);
        }
    }
}
