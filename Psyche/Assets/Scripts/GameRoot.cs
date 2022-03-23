using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using MyBox;

/// <summary>
/// This is the Motherbrain of the game.  There should only ever be one instance
/// of this class in the whole game.  All level/puzzle/game controllers should be
/// referenced in this class.  
/// The UIRoot (when it's completed) should be referenced in this class so that any
/// class can access their UI elements via this Root to the UIRoot.
/// </summary>
public class GameRoot : MonoBehaviour
{
    //GAME ASSET ROOT OBJECTS
    [HideInInspector]
    public static GameRoot _Root;


    [Header("=========== Services ===========")]
    [ReadOnly]
    public static ResourceService resourceService;
    [ReadOnly]
    public UIWindowsController windowsController;
    [ReadOnly]
    public SceneChanger sceneChanger;
    [ReadOnly]
    public SceneChanger.scenes prevScene;
    [ReadOnly]
    public SceneChanger.scenes currScene;
    [ReadOnly]
    public SceneChanger.scenes nextScene;

    [ReadOnly]
    public AudioService audioService;

    //TotalOver Game Scores
    [ReadOnly] [SerializeField] public int[] tot_neutronScores;
    [ReadOnly] [SerializeField] public float tot_radioScore;
    [ReadOnly] [SerializeField] public float tot_magnetometerScore;
    [ReadOnly] [SerializeField] public float tot_multispectScore;


    public bool isPause = false;

    private void Awake()
    {
        _Root = this;
        resourceService = this.GetComponent<ResourceService>();
        audioService = GetComponentInChildren<AudioService>();
        windowsController = GetComponentInChildren<UIWindowsController>();
        sceneChanger = this.GetComponent<SceneChanger>();
        OnGameStart();
        DontDestroyOnLoad(this);
        Time.timeScale = 2.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnSceneLoad();
    }


    public void OnGameStart()
    {
        tot_neutronScores = new int[5];
        for (int i = 0; i < tot_neutronScores.Length; i++)
        {
            tot_neutronScores[i] = 0;
        }
        tot_radioScore = 0;
        tot_magnetometerScore = 0;
        tot_multispectScore = 0;

        LoadAudioFiles();
    }


    private void LoadAudioFiles()
    {
        resourceService.LoadAudio(Constants.Audio.Gameplay.BlazingStars, true);
        resourceService.LoadAudio(Constants.Audio.Gameplay.ColdMoon, true);
        resourceService.LoadAudio(Constants.Audio.Gameplay.CyberREM, true);
        resourceService.LoadAudio(Constants.Audio.Gameplay.RetroSciFiPlanet, true);
        resourceService.LoadAudio(Constants.Audio.Gameplay.SciFiClose2, true);
        resourceService.LoadAudio(Constants.Audio.Gameplay.StarLight, true);
        resourceService.LoadAudio(Constants.Audio.Gameplay.TerraformingBegins, true);
    }


    public void OnSceneLoad()
    {
        audioService.PlayBgMusic(Constants.Audio.Gameplay.BlazingStars, true);

    }
}
