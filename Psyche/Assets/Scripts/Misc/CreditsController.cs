using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    public ScrollRect creditsScroll;
    public Button btn_Exit;
    public ScoresPanel finalScores;
    public SpectrometerScoresPanel spectromScores;

    [Range(0.5f, 5.0f)]
    public float delayTime = 3.0f;
    private float currTime = 0.0f;
    [Range(0.001f, 0.1f)]
    public float scrollSpeed = 0.05f;

    public bool generateDebugScores = false;




    // Start is called before the first frame update
    void Start()
    {
        creditsScroll.verticalScrollbar.value = 1.0f;

        if (generateDebugScores)
            TestScores();

        float sumofallscores = 0;
        float totneutron = 0;
        sumofallscores += GameRoot._Root.tot_magnetometerScore;
        sumofallscores += GameRoot._Root.tot_multispectScore;
        sumofallscores += GameRoot._Root.tot_radioScore;
        for (int i = 0; i < GameRoot._Root.tot_neutronScores.Length; i++)
        {
            sumofallscores += GameRoot._Root.tot_neutronScores[i];
            totneutron += GameRoot._Root.tot_neutronScores[i];
        }

        float mod = 1.0f;
        finalScores.SetMagnetEndScore(true, GameRoot._Root.tot_magnetometerScore * mod, (int)sumofallscores);
        finalScores.SetMultiEndScore(true, GameRoot._Root.tot_multispectScore * mod, (int)sumofallscores);
        finalScores.SetRadioEndScore(true, GameRoot._Root.tot_radioScore * mod, (int)sumofallscores);
        finalScores.SetSpectEndScore(true, totneutron * mod, (int)sumofallscores);

        spectromScores.SetAllEndScores(GameRoot._Root.tot_neutronScores, totneutron);
        finalScores.gameObject.SetActive(false);
        spectromScores.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;

        if (currTime > delayTime)
        {
            if (creditsScroll.verticalScrollbar.value > 0.0f)
            {
                creditsScroll.verticalScrollbar.value -= Time.deltaTime * scrollSpeed;
            }
            else if(GameRoot._Root.prevScene != SceneChanger.scenes.intro)
            {
                finalScores.gameObject.SetActive(true);
                spectromScores.gameObject.SetActive(true);
            }
        }
    }

    public void Click_Exit()
    {
        GameRoot._Root.audioService.PlayUIAudio(GameRoot._Root.audioLibrary.UI[0]);

        GameRoot._Root.sceneChanger.ChangeScene(SceneChanger.scenes.intro);
    }

    private void TestScores()
    {
        GameRoot._Root.prevScene = SceneChanger.scenes.level4;
        GameRoot._Root.tot_magnetometerScore = Random.Range(30.0f, 150.0f);
        GameRoot._Root.tot_multispectScore = Random.Range(30.0f, 150.0f);
        GameRoot._Root.tot_radioScore = Random.Range(30.0f, 150.0f);

        for (int i = 0; i < GameRoot._Root.tot_neutronScores.Length; i++)
        {
            GameRoot._Root.tot_neutronScores[i] = Random.Range(10, 30);
        }
    }


    public void Click_PsycheButton()
    {
        Application.OpenURL("https://psyche.asu.edu/");
    }
    public void Click_NASAButton()
    {
        Application.OpenURL("https://solarsystem.nasa.gov/missions/psyche/overview/");
    }
    public void Click_JPLButton()
    {
        Application.OpenURL("https://www.jpl.nasa.gov/missions/psyche");
    }
}
