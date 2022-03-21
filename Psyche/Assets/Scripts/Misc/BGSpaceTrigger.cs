using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpaceTrigger : MonoBehaviour
{
    public bool playerInSpace = true;

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Call GameOver conditions
            //player too far out of bounds (lost gravity contact with asteroid)

            GameRoot.player.SetActive(false);
            playerInSpace = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
