using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotate : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.forward;
    // Update is called once per frame
    void Update()
    {
        if(rotationAxis != Vector3.zero)
            this.transform.Rotate(rotationAxis.normalized, Constants.asteroidRotateSpeed);
    }
}
