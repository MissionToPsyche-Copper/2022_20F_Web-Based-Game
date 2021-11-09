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

    //AUDIO ROOT SETTINGS
    [HideInInspector]
    public static AudioListener UIAudioListener;
    public static AudioSource UIaudioSounds;
    [Range(0.0f,1.0f)]
    public static float masterVolume = 1.0f;


    public int[] neutronScores;


    private void Awake()
    {
        _Root = this;
        UIAudioListener = this.GetComponent<AudioListener>();
        UIaudioSounds = this.GetComponent<AudioSource>();
        
        
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        neutronScores = new int[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreNeutron(int index, int score)
    {
        neutronScores[index] += score;
    }



}
