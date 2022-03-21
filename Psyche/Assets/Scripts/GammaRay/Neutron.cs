using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTimer;

public class Neutron : MonoBehaviour
{
    public GameObject particle;
    private int IDindex;

    private Timer destroyTimer;
    private Timer turnToTrigger;

    private AudioSource audioEmitter;

    

    // Start is called before the first frame update
    void Start()
    {
        audioEmitter = this.GetComponent<AudioSource>();
        audioEmitter.volume = Constants.Spectrometer.Sounds.neutronEmitVolume * Constants.Audio.masterVolume;
        destroyTimer = Timer.Register(Constants.Spectrometer.Neutron.SelfDestTime, this.SelfDestruct, isLooped: false, useRealTime: false);
        turnToTrigger = Timer.Register(0.1f, this.ToggleTrigger, isLooped: false, useRealTime: false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Call some method in GameRoot/GameController to add this neutron to score
            GameRoot._Root.ScoreNeutron(IDindex, 1);

            audioEmitter.clip = GammaRayController.instance.neutronPickupFX[Random.Range(0, GammaRayController.instance.neutronPickupFX.Length)];
            audioEmitter.loop = false;
            audioEmitter.volume = Constants.Spectrometer.Sounds.neutronCollectVolume * Constants.Audio.masterVolume;

            audioEmitter.Play();
            particle.SetActive(false);
            Destroy(this.gameObject, audioEmitter.clip.length);
            //Hide the object but don't destroy it yet.
            //We need the object to still exist until its soundFX has finished playing
 //           this.SelfDestruct();
        }
    }

    public void OnDestroy()
    {
        Timer.Cancel(this.destroyTimer);
        Timer.Cancel(this.turnToTrigger);
        audioEmitter.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetID(int val)
    {
        IDindex = val;
    }

    private void SelfDestruct()
    {
        Destroy(this.gameObject);
    }

    private void ToggleTrigger()
    {
        this.GetComponent<Collider2D>().isTrigger = true;
    }

}
