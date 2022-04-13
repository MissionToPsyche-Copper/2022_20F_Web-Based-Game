using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoresWindow : MonoBehaviour
{
    [Header("========= Score Panel Objects ==========")]
    public GameObject ui_magnet;
    public GameObject ui_multi;
    public GameObject ui_radio;
    public GameObject ui_spect;

    [Header("========= Score Text Objects ==========")]
    public TextMeshProUGUI ui_orbitBonus_text;
    public TextMeshProUGUI ui_magnet_text;
    public TextMeshProUGUI ui_multispect_text;
    public TextMeshProUGUI ui_radio_text;
    public TextMeshProUGUI ui_spectrometer_text;

    [Header("========= Score Slider Objects ==========")]
    public Slider magnetSlider;
    public Slider multispectSlider;
    public Slider radioSlider;
    public Slider spectrometerSlider;


    public void InitWindow(bool orbit, bool magnet, bool multi, bool radio, bool spect)
    {
        ui_orbitBonus_text.transform.gameObject.SetActive(orbit);
        ui_magnet.SetActive(magnet);
        ui_multi.SetActive(multi);
        ui_radio.SetActive(radio);
        ui_spect.SetActive(spect);
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

    public void SetBonus(float bonus)
    {
        ui_orbitBonus_text.text = "Orbit Bonus: " + bonus.ToString("0.0") + "x";
    }

    public void ScoreNeutron(int score)
    {
        ui_spectrometer_text.text = "Spectrometer:\n" + score.ToString();
        if(score <= spectrometerSlider.maxValue)
            spectrometerSlider.value = score;
    }

    public void ScoreRadio(float score)
    {
        ui_radio_text.text = "Radio:\n" + score.ToString("0.00");
        if (score <= radioSlider.maxValue)
            radioSlider.value = score;
    }

    public void ScoreMultispect(float score)
    {
        ui_multispect_text.text = "Multispectral:\n" + score.ToString("0.00");
        if (score <= multispectSlider.maxValue)
            multispectSlider.value = score;
    }
    public void ScoreMagnetometer(float score)
    {
        ui_magnet_text.text = "Magnetometer:\n" + score.ToString("0.00");
        if (score <= magnetSlider.maxValue)
            magnetSlider.value = score;
    }
}
