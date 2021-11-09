using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTimer;

public class Neutron : MonoBehaviour
{
    private int IDindex;

    private Timer destroyTimer;
    private Timer turnToTrigger;

    private AudioSource audioEmitter;

    

    // Start is called before the first frame update
    void Start()
    {
        audioEmitter = this.GetComponent<AudioSource>();
        audioEmitter.volume *= GameRoot.masterVolume;
        destroyTimer = Timer.Register(Constants.Spectrometer.Neutron.SelfDestTime, this.SelfDestruct, isLooped: false, useRealTime: false);
        turnToTrigger = Timer.Register(0.1f, this.ToggleTrigger, isLooped: false, useRealTime: false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Call some method in GameRoot/GameController to add this neutron to score
            GameRoot._Root.ScoreNeutron(IDindex, 1);

            audioEmitter.clip = GammaRayController.neutronPickupFX;
            audioEmitter.volume = 0.5f * GameRoot.masterVolume; ;
            audioEmitter.Play();

            //Hide the object but don't destroy it yet.
            //We need the object to still exist until its soundFX has finished playing
            this.SelfDestruct();
        }
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
        Timer.Cancel(this.destroyTimer);
        Timer.Cancel(this.turnToTrigger);
        Destroy(this.gameObject);
    }

    private void ToggleTrigger()
    {
        this.GetComponent<Collider2D>().isTrigger = true;
    }

}
