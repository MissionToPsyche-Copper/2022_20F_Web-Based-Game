using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPop : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject shipObject;
    public GameObject asteroidObj;

    public float maxHeight = 20.0f;
    public float rate = 0.1f;
    private float initDistance;
    private float distanceFrom;

    void Start()
    {
        distanceFrom = (shipObject.transform.position - asteroidObj.transform.position).magnitude;
        initDistance = distanceFrom;

    }

    // Update is called once per frame
    void Update()
    {
        distanceFrom = (shipObject.transform.position - asteroidObj.transform.position).magnitude;
        if (distanceFrom > initDistance && shipObject.transform.position.z < maxHeight)
            shipObject.transform.position = new Vector3(shipObject.transform.position.x, shipObject.transform.position.y, (distanceFrom - initDistance) * rate);
    }
}
