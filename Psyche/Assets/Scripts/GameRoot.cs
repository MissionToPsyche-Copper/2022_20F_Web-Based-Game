using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

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
    [HideInInspector]
    public static GameObject player;
    [HideInInspector]
    public static GameObject mainAsteroid;

    public static ResourceService resourceService;


    //AUDIO ROOT SETTINGS
    public AudioService audioService;

    [Header("=========== Game Tools ===========")]
    //Tools
    public GameObject neutron;
    public GameObject radio;
    public GameObject magnet;
    public GameObject multispect;

    [Header("=========== UI ===========")]
    //UI Scores
    public TextMeshProUGUI ui_neutron;
    public TextMeshProUGUI ui_radio;
    public TextMeshProUGUI ui_magnet;
    public TextMeshProUGUI ui_multispect;


    [Header("=========== Scores ===========")]
    //Per Scene Scores
    [SerializeField] private int[] neutronScores;
    private int neutronTot = 0;
    [SerializeField] private float radioScore;
    [SerializeField] private float magnetometerScore;
    [SerializeField] private float multispectScore;

    //TotalOver Game Scores
    [SerializeField] private int[] tot_neutronScores;
    [SerializeField] private float tot_radioScore;
    [SerializeField] private float tot_magnetometerScore;
    [SerializeField] private float tot_multispectScore;


    public bool isPause = false;

    private void Awake()
    {
        _Root = this;
        player = GameObject.FindGameObjectWithTag("Player");
        mainAsteroid = GameObject.FindGameObjectWithTag("asteroid");
        resourceService = this.GetComponent<ResourceService>();
        audioService = GetComponentInChildren<AudioService>();
        OnGameStart();
        DontDestroyOnLoad(this);
        Time.timeScale = 2.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnSceneLoad();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnGameStart()
    {
        neutronScores = new int[5];
        tot_neutronScores = new int[5];
        for (int i = 0; i < neutronScores.Length; i++)
        {
            neutronScores[i] = 0;
            tot_neutronScores[i] = 0;
        }

        radioScore = 0;
        magnetometerScore = 0;
        multispectScore = 0;
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
        for (int i = 0; i < neutronScores.Length; i++)
        {
            neutronScores[i] = 0;
        }

        radioScore = 0;
        magnetometerScore = 0;
        multispectScore = 0;

        ui_neutron.gameObject.SetActive(neutron.activeInHierarchy);
        ui_radio.gameObject.SetActive(radio.activeInHierarchy);
        ui_magnet.gameObject.SetActive(magnet.activeInHierarchy);
        ui_multispect.gameObject.SetActive(multispect.activeInHierarchy);
        audioService.PlayBgMusic(Constants.Audio.Gameplay.BlazingStars, true);

    }

    public void ScoreNeutron(int index, int score)
    {
        neutronScores[index] += score;
        neutronTot += score;
        ui_neutron.text = "Spectrometer:\n" + neutronTot;        
    }

    public void ScoreRadio(float score)
    {
        radioScore += score;
        ui_radio.text = "Radio:\n" + radioScore.ToString("0.00");
    }

    public void ScoreMultispect(float score)
    {
        multispectScore += score;
        ui_multispect.text = "Multispectral:\n" + multispectScore.ToString("0.00");
    }
    public void ScoreMagnetometer(float score)
    {
        magnetometerScore += score;
        ui_magnet.text = "Magnetometer:\n" + magnetometerScore.ToString("0.00");
    }

    public void OnSceneChange()
    {
        for(int i = 0; i < neutronScores.Length; i++)
        {
            tot_neutronScores[i] += neutronScores[i];
            neutronScores[i] = 0;
        }
        tot_radioScore += radioScore;
        radioScore = 0;
        tot_magnetometerScore += magnetometerScore;
        magnetometerScore = 0;
        tot_multispectScore += multispectScore;
        multispectScore = 0;
    }

}
