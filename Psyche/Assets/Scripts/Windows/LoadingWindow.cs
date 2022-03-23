using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow : MonoBehaviour
{
    public Slider ShipSlider;
    public Slider AsteroidSlider;
    public Transform AsteroidHandle;


    public void OnEnable()
    {
        ShipSlider.value = 0.0f;
        AsteroidSlider.value = 0.0f;
        AsteroidHandle.localScale = new Vector3(0.2f, 0.2f, 1.0f);
    }

    public void SetProgress(float val)
    {
        ShipSlider.value = val;
        AsteroidSlider.value = val;
        AsteroidHandle.localScale = new Vector3(0.2f + (0.8f * val), 0.2f + (0.8f * val), 1.0f);
    }
}
