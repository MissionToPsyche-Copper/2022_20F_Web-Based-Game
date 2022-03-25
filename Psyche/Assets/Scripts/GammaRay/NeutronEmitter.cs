using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutronEmitter : MonoBehaviour
{
    public GammaRayController mainController;

    public List<GameObject> neutrons;


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

        for (int i = 0; i < emitNum; i++)
        {
            id = Random.Range(0, neutrons.Count);
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
