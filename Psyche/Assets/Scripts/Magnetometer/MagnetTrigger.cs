using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimitivePlus;

public class MagnetTrigger : MonoBehaviour
{
    private Magnetometer magController;
    public bool insideRing = false;
    private float messgWait = 0.0f;
    private float messgMax = 2.0f;

    public void SetMagController(Magnetometer control)
    {
        magController = control;
    }

    public void Update()
    {
        if (messgWait > messgMax)
            return;
        messgWait += Time.deltaTime;
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "magnet")
        {
            if (insideRing)
            {
                magController.SetRingStatus(false);
                if (messgWait >= messgMax)
                {
                    PopMessageUI.PopUpMessage("Exiting Magnetometer Orbit", 4.0f);
                    messgWait = 0.0f;
                }
            }
            else
            {
                magController.SetRingStatus(true);
                if (messgWait >= messgMax)
                {
                    PopMessageUI.PopUpMessage("Entering Magnetometer Orbit", 4.0f);
                    messgWait = 0.0f;
                }
            }

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "magnet")
        {
            if (insideRing)
            {
                magController.SetRingStatus(true);
                if (messgWait >= messgMax)
                {
                    PopMessageUI.PopUpMessage("Entering Magnetometer Orbit", 4.0f);
                    messgWait = 0.0f;
                }
            }
            else
            {
                magController.SetRingStatus(false);
                if (messgWait >= messgMax)
                {
                    PopMessageUI.PopUpMessage("Exiting Magnetometer Orbit", 4.0f);
                    messgWait = 0.0f;
                }
            }
        }
    }
}
