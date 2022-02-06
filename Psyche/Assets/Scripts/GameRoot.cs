using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the Motherbrain of the game.  There should only ever be one instance
/// of this class in the whole game.  All level/puzzle/game controllers should be
/// referenced in this class.  
/// The UIRoot (when it's completed) should be referenced in this class so that any
/// class can access their UI elements via this Root to the UIRoot.
/// 
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

    //AUDIO ROOT SETTINGS
    [HideInInspector]
    public static AudioListener UIAudioListener;
    public static AudioSource UIaudioSounds;
    [Range(0.0f,1.0f)]
    public static float masterVolume = 1.0f;

    //Scores
    [SerializeField] private int[] neutronScores;
    [SerializeField] private float radioScore;
    [SerializeField] private float magnetometerScore;
    [SerializeField] private float multispectScore;


    private void Awake()
    {
        _Root = this;
        player = GameObject.FindGameObjectWithTag("Player");
        mainAsteroid = GameObject.FindGameObjectWithTag("asteroid");
        UIAudioListener = this.GetComponent<AudioListener>();
        UIaudioSounds = this.GetComponent<AudioSource>();
        
        
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        neutronScores = new int[5];
        for (int i = 0; i < neutronScores.Length; i++)
            neutronScores[i] = 0;

        radioScore = 0;
        magnetometerScore = 0;
        multispectScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreNeutron(int index, int score)
    {
        neutronScores[index] += score;
    }

    public void ScoreRadio(float score)
    {
        radioScore += score;
    }

    public void ScoreMultispect(float score)
    {
        multispectScore += score;
    }
    public void ScoreMagnetometer(float score)
    {
        magnetometerScore += score;
    }


}
