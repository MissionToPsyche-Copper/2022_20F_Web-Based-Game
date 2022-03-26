using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpaceTrigger : MonoBehaviour
{
    public bool playerInSpace = true;
    private GameObject mainCamera;
    private Vector3 lostPosition;

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !LevelController.gameEnd)
        {
            //Call GameOver conditions
            //player too far out of bounds (lost gravity contact with asteroid)
            LevelController.player.GetComponent<ShipControl>().enabled = false;
            LevelController.player.GetComponent<OrbitalGravity>().enabled = false;
            lostPosition = new Vector3(LevelController.player.transform.position.x, LevelController.player.transform.position.y, LevelController.player.transform.position.z);
            playerInSpace = false;
            mainCamera.GetComponent<Camera>().orthographic = false;
            LevelController.levelRoot.BadEnd(true, "Lost Orbit with Asteroid");
        }
    }

    public void OnTriggerExit(Collider other)
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerInSpace)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, lostPosition, 0.0001f * Time.deltaTime);
            mainCamera.transform.LookAt(LevelController.player.transform, Vector3.Cross(mainCamera.transform.forward, LevelController.player.transform.right));
        }
    }
}
