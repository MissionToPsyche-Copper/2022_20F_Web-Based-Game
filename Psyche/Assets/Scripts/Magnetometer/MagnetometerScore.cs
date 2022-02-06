using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetometerScore : MonoBehaviour
{
    bool showEffect = false;
    public GameObject effect;
    public GameObject ship;
    public GameObject magneticField;
    private Vector3 scaleChange;

    public void OnTriggerEnter(Collider collider)
    {

        Debug.Log("Ship is in field");
        showEffect = true;
    }

    public void OnTriggerExit(Collider collider)
    {
        Debug.Log("Ship is out of field");
        showEffect = false;
    }

    public void resizeField()
    {
        float x = Random.Range(0f, 1f);
        scaleChange = new Vector3(x, x, x);
        if(magneticField.transform.localScale.x > 2.0f){
            magneticField.transform.localScale -= scaleChange;
            }
            else{
                magneticField.transform.localScale += scaleChange;
            };

    }

    IEnumerator Coroutine()
    {
        while(true){
            yield return new WaitForSeconds(10);
            resizeField();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Coroutine());
    }

    // Update is called once per frame
    void Update()
    {
        effect.transform.position = ship.transform.position;
        effect.SetActive(showEffect);
        
    }
}
