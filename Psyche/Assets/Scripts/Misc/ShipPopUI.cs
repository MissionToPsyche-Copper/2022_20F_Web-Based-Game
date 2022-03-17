using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPopUI : MonoBehaviour
{
    private GameObject mainCam;
    [SerializeField] private Camera chaseCam;
    [SerializeField] private Camera asteroidCam;
    [SerializeField] private GameObject shipPopUp;
    [SerializeField] private GameObject asteroidPopUp;
    [SerializeField] private GameObject shipPointer;
    [SerializeField] private int turnOnRange = 150;
    private bool showMiniMap = false;
    private bool toggleMapPos = true;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        chaseCam.transform.parent = GameRoot.player.transform;

        shipPointer.SetActive(false);
        shipPopUp.SetActive(false);
        chaseCam.enabled = false;
        asteroidPopUp.SetActive(false);
        asteroidCam.enabled = false;

    }

    private void Update()
    {
        if(showMiniMap && !toggleMapPos)
        {
            shipPointer.transform.position = GameRoot.player.transform.position + Vector3.forward * -60.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleMiniMap();
        }
        if(Input.GetKeyDown(KeyCode.E) && GameRoot._Root.magnet.activeInHierarchy)
        {
            ToggleMapFocus();
        }

        if (GameRoot.player.transform.position.magnitude > turnOnRange)
        {
            chaseCam.transform.rotation = mainCam.transform.rotation;
 //           asteroidCam.transform.rotation = mainCam.transform.rotation;

            shipPopUp.SetActive(true);
            chaseCam.enabled = true;
        }
        else
        {
            shipPopUp.SetActive(false);
            chaseCam.enabled = false;
        }
    }

    private void ToggleMiniMap()
    {
        showMiniMap = !showMiniMap;

        asteroidPopUp.SetActive(showMiniMap);
        asteroidCam.enabled = showMiniMap;
    }

    private void ToggleMapFocus()
    {
        toggleMapPos = !toggleMapPos;

        if (toggleMapPos)
        {
            asteroidCam.transform.position = GameRoot.mainAsteroid.transform.position + Vector3.back * 80;
            asteroidCam.orthographicSize = 50;
            shipPointer.SetActive(false);
        }
        else
        {
            shipPointer.SetActive(true);
            asteroidCam.transform.position = GameRoot._Root.magnet.GetComponent<MagnetometerController>().GetRingCenter() + Vector3.back * 100;
            asteroidCam.orthographicSize = GameRoot._Root.magnet.GetComponent<MagnetometerController>().GetRingSize();
            shipPointer.transform.localScale = new Vector3(asteroidCam.orthographicSize * 0.5f, asteroidCam.orthographicSize * 0.5f, 1);
            asteroidCam.orthographicSize *= 1.25f;
        }
    }
}
