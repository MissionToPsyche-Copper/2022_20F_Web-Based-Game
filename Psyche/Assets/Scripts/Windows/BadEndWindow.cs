using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BadEndWindow : MonoBehaviour
{
    public Button btn_Reset;
    public Button btn_Quit;

    public Transform scorePanel;
    public ScoresPanel scores;


    public void Click_Reset()
    {
        this.gameObject.SetActive(false);
        GameRoot._Root.sceneChanger.ChangeScene(GameRoot._Root.currScene);
        Destroy(GameRoot._Root.gameObject);        
    }

    public void Click_Quit()
    {
        
    }

}
