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
    [HideInInspector]
    public static GameRoot _Root;


    private void Awake()
    {
        _Root = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
