using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotate : MonoBehaviour
{
    public float rotateSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward, rotateSpeed);
    }
}
