using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject shipTarget;
    public GameObject body;

    private Vector3 displacement;
    private float initDistance;
    private float currDistace;
    private float initCamSize;


    // Start is called before the first frame update
    void Start()
    {
        initDistance = (shipTarget.transform.position - body.transform.position).magnitude;
        initCamSize = this.GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    public void Update()
    {
        displacement = shipTarget.transform.position - body.transform.position;
        currDistace = displacement.magnitude;

        this.transform.position = (displacement * (4.0f / 5.0f)) + Vector3.forward * (-10);
        this.GetComponent<Camera>().orthographicSize = initCamSize * (currDistace / initDistance);
    }
}
