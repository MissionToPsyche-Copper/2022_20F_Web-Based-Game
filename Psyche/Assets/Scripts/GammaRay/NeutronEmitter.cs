using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class NeutronEmitter : MonoBehaviour
{
    public GammaRayController mainController;

    public List<GameObject> neutrons;
    [MinMaxRange(0.01f, 1.0f)]
    public MinMaxFloat element1chance;
    private float element1;
    [MinMaxRange(0.01f, 1.0f)]
    public MinMaxFloat element2chance;
    private float element2;
    [MinMaxRange(0.01f, 1.0f)]
    public MinMaxFloat element3chance;
    private float element3;
    [MinMaxRange(0.01f, 1.0f)]
    public MinMaxFloat element4chance;
    private float element4;
    [MinMaxRange(0.01f, 1.0f)]
    public MinMaxFloat element5chance;
    private float element5;

    private UnityTimer.Timer selfDest;
    private AudioSource audioEmitter;


    // Start is called before the first frame update
    void Start()
    {

        audioEmitter = this.GetComponent<AudioSource>();
        audioEmitter.volume *= Constants.Audio.masterVolume;
        audioEmitter.clip = GammaRayController.rayShootFX;
    }


    public void OnTriggerEnter(Collider other)
    {
        Emit();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "asteroid")
            Emit();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.otherCollider.tag == "asteroid")
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Emit()
    {
        int id;
        int emitNum = Random.Range(1, Constants.Spectrometer.Neutron.emitMax);

        audioEmitter.clip = GammaRayController.rayCollideFX;
        audioEmitter.loop = false;
        audioEmitter.volume = 0.25f * Constants.Audio.masterVolume;
        audioEmitter.Play();

        DetermineCompositionDistribution();

        for (int i = 0; i < emitNum; i++)
        {
            float val = Random.value;

            if (val < element1)
                id = 0;
            else if (val < element2)
                id = 1;
            else if (val < element3)
                id = 2;
            else if (val < element4)
                id = 3;
            else
                id = 4;

            GameObject temp = Instantiate(neutrons[id]);

            temp.SetActive(true);
            temp.transform.position = this.transform.position;
            temp.transform.parent = mainController.neutronsList.transform;
            temp.GetComponent<Neutron>().SetID(id);
            //temp.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity ;
            temp.GetComponent<Rigidbody2D>().velocity = -RandomReflect(this.GetComponent<Rigidbody2D>().velocity).normalized * Random.Range(1.0f, 7.0f);
        }

        Destroy(this.gameObject);
    }

    private void DetermineCompositionDistribution()
    {
        //alpha values
        element1 = Random.Range(element1chance.Min, element1chance.Max) / 5.0f;
        element2 = Random.Range(element2chance.Min, element2chance.Max) / 5.0f;
        element3 = Random.Range(element3chance.Min, element3chance.Max) / 5.0f;
        element4 = Random.Range(element4chance.Min, element4chance.Max) / 5.0f;
        element5 = Random.Range(element5chance.Min, element5chance.Max) / 5.0f;

        float beta = element1 + element2 + element3 + element4 + element5;
        beta = 1.0f / beta;

        //convert values some percent of 1.0f
        element1 *= beta;
        element2 *= beta;
        element3 *= beta;
        element4 *= beta;
        element5 *= beta;

        //distribute values from 0.0 to 1.0
        element2 += element1;
        element3 += element2;
        element4 += element3;
        element5 += element4;
    }


    private Vector3 RandomReflect(Vector3 vector)
    {
        vector = vector.normalized;
        float randTheta = Random.Range(-90.0f, 90.0f) * Mathf.Deg2Rad;
        Vector3 reflect;

        reflect.x = vector.x * Mathf.Cos(randTheta) - vector.y * Mathf.Sin(randTheta);
        reflect.y = vector.x * Mathf.Sin(randTheta) + vector.y * Mathf.Cos(randTheta);
        reflect.z = 0.0f;

        return vector;

    }
}
