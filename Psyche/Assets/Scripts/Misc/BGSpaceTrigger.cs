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
        if (collision.tag == "Player" && !SceneController.gameEnd)
        {
            //Call GameOver conditions
            //player too far out of bounds (lost gravity contact with asteroid)
            SceneController.player.GetComponent<ShipControl>().enabled = false;
            SceneController.player.GetComponent<OrbitalGravity>().enabled = false;
            lostPosition = new Vector3(SceneController.player.transform.position.x, SceneController.player.transform.position.y, SceneController.player.transform.position.z);
            playerInSpace = false;
            mainCamera.GetComponent<Camera>().orthographic = false;
            SceneController.sceneRoot.BadEnd(true);
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
            mainCamera.transform.LookAt(SceneController.player.transform, Vector3.Cross(mainCamera.transform.forward, SceneController.player.transform.right));
        }
    }
}
