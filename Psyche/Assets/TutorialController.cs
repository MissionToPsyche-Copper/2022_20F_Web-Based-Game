using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public VideoController videoController;

    public void Awake()
    {
        videoController.videoName = Constants.Videos.intro;
        //        IntroScreen.gameObject.SetActive(true);
    }

    public void Start()
    {
        videoController.fadeSpeed = 0.8f;
        videoController.gameObject.SetActive(false);
    }

    public void Click_Controls()
    {
        videoController.gameObject.SetActive(true);

        videoController.RestartVideo(Constants.Videos.controls);

    }


    public void Click_Magnet()
    {
        videoController.gameObject.SetActive(true);

        videoController.RestartVideo(Constants.Videos.magnet);

    }

    public void Click_Multispect()
    {
        videoController.gameObject.SetActive(true);

        videoController.RestartVideo(Constants.Videos.multispect);
    }
    public void Click_Radio()
    {
        videoController.gameObject.SetActive(true);

        videoController.RestartVideo(Constants.Videos.radio);
    }

    public void Click_Spectrometer()
    {
        videoController.gameObject.SetActive(true);

        videoController.RestartVideo(Constants.Videos.spectrometer);
    }

    public void Click_Back()
    {
        GameRoot._Root.prevScene = SceneChanger.scenes.tutorial;
        GameRoot._Root.currScene = SceneChanger.scenes.intro;
        GameRoot._Root.sceneChanger.ChangeScene(SceneChanger.scenes.intro);
    }


}
