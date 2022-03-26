using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShipMove : MonoBehaviour
{
    public GameObject ship;
    public Transform camera;
    public GameObject light1;
    public GameObject light2;
    private float initLineWidth;
    private float rotateSpeed = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        initLineWidth = ship.GetComponentInChildren<TrailRenderer>().startWidth;
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        RandomShip();
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == ship)
        {
            RandomShip();
        }
    }

    public void Update()
    {
        //if (rotateSpeed > 0.05f)
        //    rotateSpeed -= 0.005f;
        //Quaternion rotation = Quaternion.LookRotation(ship.transform.position - camera.position, Vector3.up);
        //camera.rotation = Quaternion.RotateTowards(camera.rotation, rotation , rotateSpeed);

        ship.transform.localScale = Vector3.one * (500 - (ship.transform.position - camera.localPosition).magnitude) / 50.0f;
        ship.GetComponentInChildren<TrailRenderer>().startWidth = initLineWidth * (500 - (ship.transform.position - camera.localPosition).magnitude) / 50.0f;
        if (Random.value < 0.01f)
            light1.SetActive(!light1.activeInHierarchy);
        if (Random.value < 0.005f)
        {
            light2.SetActive(!light2.activeInHierarchy);


            //GameObject sparkles = Instantiate(light2);
            //sparkles.transform.position = ship.transform.position;
            //sparkles.transform.rotation = Quaternion.Euler(Random.onUnitSphere);
            //sparkles.transform.localScale *= Random.Range(1.0f, 3.0f) * ship.transform.localScale.x;
            //sparkles.GetComponent<ParticleSystem>().Play();
            //Destroy(sparkles, 1.0f);
        }
        if (light2.activeInHierarchy)
            ship.GetComponent<Rigidbody>().AddForce(ship.transform.up * Random.Range(10.0f, 20.0f), ForceMode.Acceleration);
        else
            ship.GetComponentInChildren<TrailRenderer>().Clear();



    }

    private void RandomShip()
    {
        ship.transform.position = Random.insideUnitSphere * this.transform.localScale.x * 0.5f;
        ship.GetComponent<Rigidbody>().velocity = -(ship.transform.position - this.transform.position).normalized * Random.Range(40, 70);
        ship.transform.rotation = Quaternion.LookRotation(ship.transform.position - this.transform.position, ship.transform.forward);


        //rotateSpeed = 0.3f;

        float val = Random.value;

        if (val < 0.4f)
            ship.GetComponent<ObjectRotate>().rotationAxis = Vector3.up;
        else if (val < 0.8f)
            ship.GetComponent<ObjectRotate>().rotationAxis = Vector3.left;
        else 
            ship.GetComponent<ObjectRotate>().rotationAxis = Random.insideUnitSphere;

        ship.GetComponent<ObjectRotate>().rotationSpeed = Random.Range(0.2f, 0.8f);
        ship.GetComponentInChildren<TrailRenderer>().Clear();

    }
}
