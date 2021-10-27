using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FaceObject : MonoBehaviour
{
    public GameObject target;
    private Vector3 axis = Vector3.left;


    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
            this.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 modTarget = new Vector3(target.transform.position.z, 0.0f, target.transform.position.z);
        
        this.transform.LookAt(modTarget, axis);
    }

    public void SetTarget(GameObject obj)
    {
        target = obj;
        this.enabled = true;
    }

    public void SetTarget(Vector3 vect)
    {
        target.transform.position = vect;
        this.enabled = true;
    }

    public void SetRotationAxis(Vector3 vect)
    {
        axis = vect;
    }
}
