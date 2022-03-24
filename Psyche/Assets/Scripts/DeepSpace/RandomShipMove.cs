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

    // Start is called before the first frame update
    void Start()
    {
        initLineWidth = ship.GetComponentInChildren<TrailRenderer>().startWidth;

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
        ship.transform.localScale = Vector3.one * (500 - (ship.transform.position - camera.localPosition).magnitude) / 50.0f;
        ship.GetComponentInChildren<TrailRenderer>().startWidth = initLineWidth * (500 - (ship.transform.position - camera.localPosition).magnitude) / 50.0f;
        if (Random.value < 0.01f)
            light1.SetActive(!light1.activeInHierarchy);
        if (Random.value < 0.001f)
        {
            GameObject sparkles = Instantiate(light2);
            sparkles.transform.position = ship.transform.position;
            sparkles.transform.rotation = Quaternion.Euler(Random.onUnitSphere);
            sparkles.transform.localScale *= Random.Range(1.0f, 3.0f) * ship.transform.localScale.x;
            sparkles.GetComponent<ParticleSystem>().Play();
            Destroy(sparkles, 1.0f);
        }
    }

    private void RandomShip()
    {
        ship.transform.position = Random.insideUnitSphere * this.transform.localScale.x * 0.5f;
        ship.GetComponent<Rigidbody>().velocity = -(ship.transform.position - this.transform.position).normalized * Random.Range(40, 70) + Random.insideUnitSphere * this.transform.localScale.x * 0.01f;
        ship.transform.LookAt(this.transform);

        float val = Random.value;

        if (val < 0.5f)
            ship.GetComponent<ObjectRotate>().rotationAxis = Vector3.up;
        else if (val < 0.98f)
            ship.GetComponent<ObjectRotate>().rotationAxis = Vector3.left;
        else 
            ship.GetComponent<ObjectRotate>().rotationAxis = Random.insideUnitSphere;

        ship.GetComponent<ObjectRotate>().rotationSpeed = Random.Range(0.2f, 1.0f);
        ship.GetComponentInChildren<TrailRenderer>().Clear();

    }
}
