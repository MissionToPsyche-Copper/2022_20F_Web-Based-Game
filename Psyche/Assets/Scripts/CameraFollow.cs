using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject shipTarget;
    public GameObject body;
    public bool cameraRotate = false;

    private Vector3 displacement;
    private float initDistance;
    private float currDistace;
    private float initCamSize;


    // Start is called before the first frame update
    void Start()
    {
        displacement = shipTarget.transform.position - body.transform.position;

        initDistance = displacement.magnitude;
        currDistace = displacement.magnitude;

        initCamSize = currDistace / 2.0f;
    }

    // Update is called once per frame
    public void Update()
    {
        displacement = shipTarget.transform.position - body.transform.position;
        currDistace = displacement.magnitude;

        this.transform.position = (displacement * (2.0f / 3.0f)) + Vector3.forward * (-40);
        this.GetComponent<Camera>().orthographicSize = initCamSize * (currDistace / initDistance) * 1.1f;

        if (cameraRotate)
            RotateCamera();
    }


    private void RotateCamera()
    {    //Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg
        float angle = Vector3.SignedAngle(Vector3.up, displacement, Vector3.forward) ;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, 10.0f);
    }
}
