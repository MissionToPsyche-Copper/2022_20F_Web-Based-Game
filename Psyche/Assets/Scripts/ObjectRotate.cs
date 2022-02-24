using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class ObjectRotate : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.forward;
    [ReadOnly]
    public float rotationSpeed = Constants.Space.objRotateSpeed;

    private void Start()
    {
        rotationSpeed = Constants.Space.objRotateSpeed;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(rotationAxis != Vector3.zero)
            this.transform.Rotate(rotationAxis.normalized, rotationSpeed);
    }

    public void SetSpeed(float val)
    {
        rotationSpeed = val;
    }

    public void SetAxis(Vector3 val)
    {
        rotationAxis = val;
    }
}
