using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class OrbitalGravity : MonoBehaviour
{
    public GameObject body;
    public bool massFactor;
    [ConditionalField("massFactor")]
    public int factor = 1;
    private Vector3 displacement;
    private Vector2 normalUnit;
    private float radius;
    private float gravConst;
    private double massByMass;
    [ReadOnly]
    public Vector2 force;



    //public void Awake()
    //{
    //    displacement = this.transform.position - body.transform.position;
    //    normalUnit = new Vector2(displacement.x, displacement.y).normalized;
    //    radius = displacement.magnitude;

    //    Physics2D.gravity = normalUnit * (gravConst * (massByMass / Mathf.Pow(radius, 2)));
    //}

    // Start is called before the first frame update
    void Start()
    {
        gravConst = 6.6743f * Mathf.Pow(10, -2.5f);
        if(massFactor)
            massByMass = factor * body.GetComponent<Rigidbody2D>().mass * this.GetComponent<Rigidbody2D>().mass;
        else
            massByMass = body.GetComponent<Rigidbody2D>().mass * this.GetComponent<Rigidbody2D>().mass;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        displacement = this.transform.position - body.transform.position;
        normalUnit = new Vector2(displacement.x, displacement.y).normalized;
        radius = displacement.magnitude;
        force = -normalUnit * (gravConst * (float)(massByMass / Mathf.Pow(radius, 2)));
        this.GetComponent<Rigidbody2D>().AddForce(force);

    }
}
