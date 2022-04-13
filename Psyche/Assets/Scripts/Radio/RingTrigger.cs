using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class RingTrigger : MonoBehaviour
{
    [ReadOnly]
    public RadioCollision manager;    
    [ReadOnly]
    public  int ringID = 0;
    [ReadOnly]
    public bool inRing;

    private float messgTimeOut = 0.0f;


    public void Start()
    {
        manager = this.transform.parent.gameObject.GetComponent<RadioCollision>();
        inRing = false;
    }

    public void Update()
    {
        if (messgTimeOut <= 0.0f)
            return;

        messgTimeOut -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            manager.CurrentRing(ringID);
            inRing = true;

            if (messgTimeOut <= 0.0f)
            {
                float alt = this.GetComponent<CircleCollider2D>().radius - LevelController.mainAsteroid.GetComponent<PolygonCollider2D>().bounds.extents.x;
                PopMessageUI.PopUpMessage("Radio Science Altitude: " + (alt * Constants.Space.AltitudeFactorAdjust).ToString("0.0"), 4.0f);
                messgTimeOut = 1.0f;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            manager.CurrentRing(ringID - 1);
            inRing = false;
        }
    }

}
