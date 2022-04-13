using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button btn_Start;
    public Button btn_Tutorial;
    public Button btn_Credits;
    public Button btn_Retart;
    public Button btn_Logos;


    public void Click_Start()
    {
        GameRoot._Root.prevScene = SceneChanger.scenes.intro;
        GameRoot._Root.OnGameStart();
        GameRoot._Root.audioService.PlayUIAudio(GameRoot._Root.audioLibrary.UI[0]);
        GameRoot._Root.sceneChanger.ChangeScene(SceneChanger.scenes.level1);
    }
    public void Click_Tutorial()
    {
        GameRoot._Root.prevScene = SceneChanger.scenes.intro;
        GameRoot._Root.audioService.PlayUIAudio(GameRoot._Root.audioLibrary.UI[0]);
        GameRoot._Root.sceneChanger.ChangeScene(SceneChanger.scenes.tutorial);

    }
    public void Click_Credits()
    {
        GameRoot._Root.prevScene = SceneChanger.scenes.intro;
        GameRoot._Root.audioService.PlayUIAudio(GameRoot._Root.audioLibrary.UI[0]);
        GameRoot._Root.sceneChanger.ChangeScene(SceneChanger.scenes.credits);
    }
    public void Click_Restart()
    {
        GameRoot._Root.audioService.PlayUIAudio(GameRoot._Root.audioLibrary.UI[0]);
        IntroController.introRoot.IntroScreen.RestartVideo(Constants.Videos.intro);
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
