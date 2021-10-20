using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward, Constants.asteroidRotateSpeed);
    }
}
