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
        audioEmitter.volume *= GameRoot.masterVolume;
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
        audioEmitter.volume = 0.25f * GameRoot.masterVolume; ;
        audioEmitter.Play();

        for (int i = 0; i < emitNum; i++)
        {
            id = Random.Range(0, neutrons.Count);
            GameObject temp = Instantiate(neutrons[id]);
            temp.SetActive(true);
            temp.transform.position = this.transform.position;
            temp.transform.parent = mainController.neutronsList.transform;
            temp.GetComponent<Neutron>().SetID(id);
            temp.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity * Random.Range(0.2f, 1.0f);
        }

        Destroy(this.gameObject);
    }

}
