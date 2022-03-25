using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    public GameObject explosion;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !SceneController.gameEnd)
        {
            //badend
            Time.timeScale = 0.3f;
            GameObject explode = Instantiate(explosion);
            explode.transform.position = SceneController.player.transform.position;
            explode.transform.localScale *= 25.0f;
            explode.transform.SetParent(SceneController.mainAsteroid.transform);
            explode.SetActive(true);
            SceneController.sceneRoot.BadEnd(false);
        }
    }

}
