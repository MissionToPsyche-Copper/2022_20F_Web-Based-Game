using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) //layer 6 is the neutrons layer
        {
            //  .normalized * Constants.Spectrometer.TractorBeam.BeamStrength
            Vector2 dirToCollector = ShipControl.shipAnt.position - collision.transform.position;
            float forceOffsetByDistance = Mathf.Pow(3.0f, (Constants.Spectrometer.TractorBeam.BeamLength * 2.5f) / dirToCollector.magnitude);
            if (forceOffsetByDistance > 100.0f)
                forceOffsetByDistance = 100.0f;
            collision.GetComponent<Rigidbody2D>().velocity /= forceOffsetByDistance;
            
            collision.GetComponent<Rigidbody2D>().AddForceAtPosition(-dirToCollector.normalized * Constants.Spectrometer.TractorBeam.BeamStrength * forceOffsetByDistance, ShipControl.shipAnt.position, ForceMode2D.Force);
        }
    }
}
