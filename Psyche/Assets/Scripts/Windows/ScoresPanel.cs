using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoresPanel : MonoBehaviour
{
    [Header("Magnetometer")]
    public Transform magnetometer;
    public TextMeshProUGUI magnet_Score;
    public Slider magnet_Slider;

    [Header("MultiSpectral")]
    public Transform multipspectral;
    public TextMeshProUGUI multipspectral_Score;
    public Slider multipspectral_Slider;

    [Header("Radio")]
    public Transform radio;
    public TextMeshProUGUI radio_Score;
    public Slider radio_Slider;

    [Header("Spectrometer")]
    public Transform spectrometer;
    public TextMeshProUGUI spectrometer_Score;
    public Slider spectrometer_Slider;

    public void Start()
    {
    }

    public void SetMagnetEndScore(bool enabled, float score, int goal)
    {
        magnetometer.gameObject.SetActive(enabled);
        magnet_Score.text = score.ToString("0.0") + "mb";
        magnet_Slider.maxValue = goal;
        magnet_Slider.value = score;
    }
    public void SetMultiEndScore(bool enabled, float score, int goal)
    {
        multipspectral.gameObject.SetActive(enabled);
        multipspectral_Score.text = score.ToString("0.0") + "mb";
        multipspectral_Slider.maxValue = goal;
        multipspectral_Slider.value = score;
    }
    public void SetRadioEndScore(bool enabled, float score, int goal)
    {
        radio.gameObject.SetActive(enabled);
        radio_Score.text = score.ToString("0.0") + "mb";
        radio_Slider.maxValue = goal;
        radio_Slider.value = score;
    }
    public void SetSpectEndScore(bool enabled, float score, int goal)
    {
        spectrometer.gameObject.SetActive(enabled);
        spectrometer_Score.text = score.ToString("0.0") + "mb";
        spectrometer_Slider.maxValue = goal;
        spectrometer_Slider.value = score;
    }

}
