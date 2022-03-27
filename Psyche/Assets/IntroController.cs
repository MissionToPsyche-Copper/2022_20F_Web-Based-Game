using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class IntroController : MonoBehaviour
{
    public static IntroController introRoot;
    public MainMenu MainMenu;
    public IntroScreen IntroScreen;

    public void Awake()
    {
        introRoot = this;
//        IntroScreen.gameObject.SetActive(true);
    }


}
