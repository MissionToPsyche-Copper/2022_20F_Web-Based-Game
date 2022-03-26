using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class LevelController : MonoBehaviour
{
    [HideInInspector]
    public static LevelController levelRoot;
    [HideInInspector]
    public static GameObject player;
    [HideInInspector]
    public static GameObject mainAsteroid;

    public SceneChanger.scenes currScene;
    public SceneChanger.scenes nextScene;


    [Header("=========== Game Rules ===========")]
    public bool magnetometerOn;
    [ConditionalField(nameof(magnetometerOn))]
    [MinMaxRange(0, 100)]
    public MinMaxInt magnetGoalminMax = new MinMaxInt(40,60);
    private int magnetGoalScore = 50;
    public bool multipspectralOn;
    [ConditionalField(nameof(multipspectralOn))]
    [MinMaxRange(0, 100)]
    public MinMaxInt multipspectralGoalminMax = new MinMaxInt(40, 60);
    private int multipspectralGoalScore = 50;
    public bool radioOn;
    [ConditionalField(nameof(radioOn))]
    [MinMaxRange(0, 100)]
    public MinMaxInt radioGoalminMax = new MinMaxInt(40, 60);
    private int radioGoalScore = 50;
    public bool spectrometerOn;
    [ConditionalField(nameof(spectrometerOn))]
    [MinMaxRange(0, 100)]
    public MinMaxInt spectrometerGoalminMax = new MinMaxInt(40, 60);
    private int spectrometerGoalScore = 50;

    private bool magnetGoalWon = false;
    private bool multispectGoalWon = false;
    private bool radioGoalWon = false;
    private bool spectrometerGoalWon = false;
    public static bool gameEnd;
    //Tools
    public GameObject magnet;
    public GameObject multispect;
    public GameObject radio;
    public GameObject spectrometer;

    [Header("=========== UI ===========")]
    public UIWindowsController windowsController;


    [Header("=========== Scores ===========")]
    //Per Scene Scores
    [SerializeField] private int[] neutronScores;
    private int neutronTot = 0; //This is an extra var holder specific to Neutron since neutron has several "types"
    [SerializeField] private float radioScore;
    [SerializeField] private float magnetometerScore;
    [SerializeField] private float multispectScore;


    public void Awake()
    {
        levelRoot = this;
        player = GameObject.FindGameObjectWithTag("Player");
        mainAsteroid = GameObject.FindGameObjectWithTag("asteroid");
        gameEnd = false;

    }

    public void Start()
    {
        OnSceneLoad();
        PlayPopUpGoals();
    }

    public void GoodEnd()
    {
        gameEnd = true;
        player.GetComponent<ShipControl>().enabled = false;

        windowsController.goodEndWindow.gameObject.SetActive(true);

        windowsController.badEndWindow.gameObject.SetActive(false);
        windowsController.shipPopWindow.gameObject.SetActive(false);
        windowsController.popUpMessages.gameObject.SetActive(false);

        windowsController.goodEndWindow.scores.SetMagnetEndScore(magnetometerOn, magnetometerScore, magnetGoalScore);
        windowsController.goodEndWindow.scores.SetRadioEndScore(radioOn, radioScore, radioGoalScore);
        windowsController.goodEndWindow.scores.SetMultiEndScore(multipspectralOn, multispectScore, multipspectralGoalScore);
        windowsController.goodEndWindow.scores.SetSpectEndScore(spectrometerOn, neutronTot, spectrometerGoalScore);

    }

    public void BadEnd(bool disable, string reason)
    {
        gameEnd = true;
        player.SetActive(disable);
        player.GetComponent<ShipControl>().enabled = false;

        windowsController.badEndWindow.gameObject.SetActive(true);
        windowsController.badEndWindow.SetBadEndReason(reason);

        windowsController.goodEndWindow.gameObject.SetActive(false);
        windowsController.shipPopWindow.gameObject.SetActive(false);
        windowsController.popUpMessages.gameObject.SetActive(false);

        windowsController.scoresWindow.gameObject.SetActive(false);
        windowsController.badEndWindow.scores.SetMagnetEndScore(magnetometerOn, magnetometerScore, magnetGoalScore);
        windowsController.badEndWindow.scores.SetRadioEndScore(radioOn, radioScore, radioGoalScore);
        windowsController.badEndWindow.scores.SetMultiEndScore(multipspectralOn, multispectScore, multipspectralGoalScore);
        windowsController.badEndWindow.scores.SetSpectEndScore(spectrometerOn, neutronTot, spectrometerGoalScore);

    }

    //The individual win checks are performed when scores are added, this is the final win condition check
    //Essentially, if a tool is not active for this level, we just assume they already won, otherwise
    //the only way to turn the tool win flag on is when adding points
    public void LevelWinCheck()
    {
        if (gameEnd)
            return;

        if (!magnetometerOn)
            magnetGoalWon = true;
        if (!multipspectralOn)
            multispectGoalWon = true;
        if (!radioOn)
            radioGoalWon = true;
        if (!spectrometerOn)
            spectrometerGoalWon = true;

        if (magnetGoalWon && multispectGoalWon && radioGoalWon && spectrometerGoalWon)
            GoodEnd();
    }

    public void OnSceneLoad()
    {
        gameEnd = false;
        GameRoot._Root.currScene = currScene;
        GameRoot._Root.nextScene = nextScene;

        windowsController.goodEndWindow.nextScene = nextScene;
        player.GetComponent<ShipControl>().enabled = true;
        Time.timeScale = 2.0f;

        neutronScores = new int[5];
        //Score Resets
        for (int i = 0; i < neutronScores.Length; i++)
        {
            neutronScores[i] = 0;
        }
        radioScore = 0;
        magnetometerScore = 0;
        multispectScore = 0;

        //Win Condition Resets
        magnetGoalWon = false;
        multispectGoalWon = false;
        radioGoalWon = false;
        spectrometerGoalWon = false;

        //Tool Turn On/Off
        magnet.SetActive(magnetometerOn);
        multispect.SetActive(multipspectralOn);
        radio.SetActive(radioOn);
        spectrometer.SetActive(spectrometerOn);

        if (magnetometerOn)
            magnetGoalScore = Random.Range(magnetGoalminMax.Min, magnetGoalminMax.Max);
        if (multipspectralOn)
            multipspectralGoalScore = Random.Range(multipspectralGoalminMax.Min, multipspectralGoalminMax.Max);
        if (radioOn)
            radioGoalScore = Random.Range(radioGoalminMax.Min, radioGoalminMax.Max);
        if (spectrometerOn)
            spectrometerGoalScore = Random.Range(spectrometerGoalminMax.Min, spectrometerGoalminMax.Max);


        windowsController.shipPopWindow.gameObject.SetActive(true);
        windowsController.popUpMessages.gameObject.SetActive(true);
        windowsController.scoresWindow.InitWindow(magnetometerOn, multipspectralOn, radioOn, spectrometerOn);
        windowsController.scoresWindow.InitSliders(magnetometerOn ? magnetGoalScore : 0,
                                                    multipspectralOn ? multipspectralGoalScore : 0,
                                                    radioOn ? radioGoalScore : 0,
                                                    spectrometerOn ? spectrometerGoalScore : 0
            );
    }

    private void PlayPopUpGoals()
    {
        if (magnetometerOn)
            PopMessageUI.PopUpMessage("New Goal: Measure " + magnetGoalScore.ToString() + "mb with Magnetometer Tool", 15.0f);
        if (multipspectralOn)
            PopMessageUI.PopUpMessage("New Goal: Measure " + multipspectralGoalScore.ToString() + "mb with Multispectral Tool", 15.0f);
        if (radioOn)
            PopMessageUI.PopUpMessage("New Goal: Measure " + radioGoalScore.ToString() + "mb with Radio Science Kit", 15.0f);
        if (spectrometerOn)
            PopMessageUI.PopUpMessage("New Goal: Measure " + spectrometerGoalScore.ToString() + "mb with Spectrometer", 15.0f);
    }

    public void ScoreNeutron(int index, int score)
    {
        if (gameEnd) //we don't want the player to gain points after the stage is over
            return;

        neutronScores[index] += score;
        neutronTot += score;
        windowsController.scoresWindow.ScoreNeutron(neutronTot);
        if (spectrometerOn && neutronTot >= spectrometerGoalScore)
            spectrometerGoalWon = true;
        LevelWinCheck();
    }

    public void ScoreRadio(float score)
    {
        if (gameEnd) //we don't want the player to gain points after the stage is over
            return;

        radioScore += score;
        windowsController.scoresWindow.ScoreRadio(radioScore);
        if (radioOn && radioScore >= radioGoalScore)
            radioGoalWon = true;
        LevelWinCheck();
    }

    public void ScoreMultispect(float score)
    {
        if (gameEnd) //we don't want the player to gain points after the stage is over
            return;

        multispectScore += score;
        windowsController.scoresWindow.ScoreMultispect(multispectScore);
        if (multipspectralOn && multispectScore >= multipspectralGoalScore)
            multispectGoalWon = true;
        LevelWinCheck();
    }

    public void ScoreMagnetometer(float score)
    {
        if (gameEnd) //we don't want the player to gain points after the stage is over
            return;

        magnetometerScore += score;
        windowsController.scoresWindow.ScoreMagnetometer(magnetometerScore);
        if (magnetometerOn && magnetometerScore >= magnetGoalScore)
            magnetGoalWon = true;
        LevelWinCheck();
    }

    /// <summary>
    /// Only call this when changing to the next level
    /// </summary>
    public void OnSceneChange()
    {
        Time.timeScale = 1.0f;
        GameRoot._Root.prevScene = currScene;

        for (int i = 0; i < neutronScores.Length; i++)
        {
            GameRoot._Root.tot_neutronScores[i] += neutronScores[i];
            neutronScores[i] = 0;
        }
        GameRoot._Root.tot_radioScore += radioScore;
        radioScore = 0;
        GameRoot._Root.tot_magnetometerScore += magnetGoalScore;
        magnetometerScore = 0;
        GameRoot._Root.tot_multispectScore += multispectScore;
        multispectScore = 0;
    }


}
