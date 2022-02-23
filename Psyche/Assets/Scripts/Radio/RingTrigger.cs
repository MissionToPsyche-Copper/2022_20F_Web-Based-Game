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
    private float waitTime = 0.0f;
    private float maxTime = 2.0f;

    public void Start()
    {
        manager = this.transform.parent.gameObject.GetComponent<RadioCollision>();
        inRing = false;
    }

    public void Update()
    {
        if (waitTime > maxTime)
            return;
        waitTime += Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            float alt = this.GetComponent<CircleCollider2D>().radius - (GameRoot.mainAsteroid.GetComponent<CircleCollider2D>().radius * GameRoot.mainAsteroid.transform.localScale.x);
            manager.CurrentRing(ringID);
            if (waitTime >= maxTime)
            {
                PopMessageUI.PopUpMessage("Radio Science Altitude: " + alt.ToString("0.0"), 4.0f);
                waitTime = 0.0f;
            }
            inRing = true;
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
