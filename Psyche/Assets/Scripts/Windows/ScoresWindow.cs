using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoresWindow : MonoBehaviour
{
    public TextMeshProUGUI ui_magnet;
    public TextMeshProUGUI ui_multispect;
    public TextMeshProUGUI ui_radio;
    public TextMeshProUGUI ui_spectrometer;

    public Slider magnetSlider;
    public Slider multispectSlider;
    public Slider radioSlider;
    public Slider spectrometerSlider;


    public void InitWindow(bool magnet, bool multi, bool radio, bool spect)
    {
        ui_magnet.transform.gameObject.SetActive(magnet);
        ui_multispect.transform.gameObject.SetActive(multi);
        ui_radio.transform.gameObject.SetActive(radio);
        ui_spectrometer.transform.gameObject.SetActive(spect);
    }

    public void InitSliders(int magnet, int multi, int radio, int spect)
    {
        magnetSlider.maxValue = magnet;
        magnetSlider.value = 0.0f;

        multispectSlider.maxValue = multi;
        multispectSlider.value = 0.0f;

        radioSlider.maxValue = radio;
        radioSlider.value = 0.0f;

        spectrometerSlider.maxValue = spect;
        spectrometerSlider.value = 0.0f;
    }

    public void ScoreNeutron(int score)
    {
        ui_spectrometer.text = "Spectrometer:\n" + score.ToString();
        if(score <= spectrometerSlider.maxValue)
            spectrometerSlider.value = score;
    }

    public void ScoreRadio(float score)
    {
        ui_radio.text = "Radio:\n" + score.ToString("0.00");
        if (score <= radioSlider.maxValue)
            radioSlider.value = score;
    }

    public void ScoreMultispect(float score)
    {
        ui_multispect.text = "Multispectral:\n" + score.ToString("0.00");
        if (score <= multispectSlider.maxValue)
            multispectSlider.value = score;
    }
    public void ScoreMagnetometer(float score)
    {
        ui_magnet.text = "Magnetometer:\n" + score.ToString("0.00");
        if (score <= magnetSlider.maxValue)
            magnetSlider.value = score;
    }
}
