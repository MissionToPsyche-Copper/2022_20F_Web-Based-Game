using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpectrometerScoresPanel : MonoBehaviour
{
    [Header("Element1")]
    public Transform Element1;
    public TextMeshProUGUI Element1_Score;
    public Slider Element1_Slider;

    [Header("Element2")]
    public Transform Element2;
    public TextMeshProUGUI Element2_Score;
    public Slider Element2_Slider;

    [Header("Element3")]
    public Transform Element3;
    public TextMeshProUGUI Element3_Score;
    public Slider Element3_Slider;

    [Header("Element4")]
    public Transform Element4;
    public TextMeshProUGUI Element4_Score;
    public Slider Element4_Slider;

    [Header("Element5")]
    public Transform Element5;
    public TextMeshProUGUI Element5_Score;
    public Slider Element5_Slider;

    public void Start()
    {
    }

    public void SetElement1EndScore(bool enabled, float score, float goal)
    {
        Element1.gameObject.SetActive(enabled);
        Element1_Score.text = ((score / goal) * 100.0f).ToString("0.0") + "%";
        Element1_Slider.maxValue = goal;
        Element1_Slider.value = score;
    }
    public void SetElement2EndScore(bool enabled, float score, float goal)
    {
        Element2.gameObject.SetActive(enabled);
        Element2_Score.text = ((score / goal) * 100.0f).ToString("0.0") + "%";
        Element2_Slider.maxValue = goal;
        Element2_Slider.value = score;
    }
    public void SetElement3EndScore(bool enabled, float score, float goal)
    {
        Element3.gameObject.SetActive(enabled);
        Element3_Score.text = ((score / goal) * 100.0f).ToString("0.0") + "%";
        Element3_Slider.maxValue = goal;
        Element3_Slider.value = score;
    }
    public void SetElement4EndScore(bool enabled, float score, float goal)
    {
        Element4.gameObject.SetActive(enabled);
        Element4_Score.text = ((score / goal) * 100.0f).ToString("0.0") + "%";
        Element4_Slider.maxValue = goal;
        Element4_Slider.value = score;
    }
    public void SetElement5EndScore(bool enabled, float score, float goal)
    {
        Element5.gameObject.SetActive(enabled);
        Element5_Score.text = ((score / goal) * 100.0f).ToString("0.0") + "%";
        Element5_Slider.maxValue = goal;
        Element5_Slider.value = score;
    }

    public void SetAllEndScores(int[] scores, float totScore)
    {
        for(int i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 0:
                    SetElement1EndScore(true, scores[i], totScore);
                    break;
                case 1:
                    SetElement2EndScore(true, scores[i], totScore);
                    break;
                case 2:
                    SetElement3EndScore(true, scores[i], totScore);
                    break;
                case 3:
                    SetElement4EndScore(true, scores[i], totScore);
                    break;
                case 4:
                    SetElement5EndScore(true, scores[i], totScore);
                    break;
            }
        }
    }
}
