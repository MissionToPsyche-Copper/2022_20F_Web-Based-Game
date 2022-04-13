using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimitivePlus;

public class OrbitTrigger : MonoBehaviour
{
    public bool insideRing = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "orbit")
        {
            if (insideRing)
            {
                Orbit.SetOrbitStatus(false);
                PopMessageUI.PopUpMessage("Exiting Mission Orbit", 4.0f);
            }
            else
            {
                Orbit.SetOrbitStatus(true);
                PopMessageUI.PopUpMessage("Entering Mission Orbit", 4.0f);
            }

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "orbit")
        {
            if (insideRing)
            {
                Orbit.SetOrbitStatus(true);
                PopMessageUI.PopUpMessage("Entering Mission Orbit", 4.0f);
            }
            else
            {
                Orbit.SetOrbitStatus(false);
                PopMessageUI.PopUpMessage("Exiting Mission Orbit", 4.0f);
            }
        }
    }
}