using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    public GameObject explosion;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !LevelController.gameEnd)
        {
            //badend
            Time.timeScale = 0.3f;
            GameObject explode = Instantiate(explosion);
            explode.transform.position = LevelController.player.transform.position;
            explode.transform.localScale *= 25.0f;
            explode.transform.SetParent(LevelController.mainAsteroid.transform);
            explode.SetActive(true);
            LevelController.levelRoot.BadEnd(false, "Probe Destroyed");
        }
        else
        {
 //           collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        }
    }

}
