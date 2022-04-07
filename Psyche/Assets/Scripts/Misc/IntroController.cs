using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class IntroController : MonoBehaviour
{
    public static IntroController introRoot;
    public MainMenu MainMenu;
    public VideoController IntroScreen;

    public void Awake()
    {
        introRoot = this;
        IntroScreen.videoName = Constants.Videos.intro;
//        IntroScreen.gameObject.SetActive(true);
    }

    public void Start()
    {
        IntroScreen.StartVideo(Constants.Videos.intro);
    }


}
