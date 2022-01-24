using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioCollision : MonoBehaviour
{

    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        effect.GetComponent<ParticleSystem>().enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        effect.GetComponent<ParticleSystem>().enableEmission = true;
    }

    void OnTriggerExit2D()
    {
        //stopEffect(); // not executing for some reason
        effect.GetComponent<ParticleSystem>().enableEmission = false;
    }

    IEnumerator stopEffect()
    {
        yield return new WaitForSeconds(.4f);
        effect.GetComponent<ParticleSystem>().enableEmission = false;
    }
}
