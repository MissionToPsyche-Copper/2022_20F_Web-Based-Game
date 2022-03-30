using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class BadEndWindow : MonoBehaviour
{
    public TextMeshProUGUI reason;

    public Button btn_Reset;
    public Button btn_Quit;

    public Transform scorePanel;
    public ScoresPanel scores;

    public void SetBadEndReason(string text)
    {
        reason.text = text;
    }

    public void Click_Reset()
    {
        this.gameObject.SetActive(false);
        PopMessageUI.ClearMessages();
        LevelController.levelRoot.OnSceneChange();
        GameRoot._Root.sceneChanger.ChangeScene(GameRoot._Root.currScene);
//        Destroy(GameRoot._Root.gameObject);        
    }

    public void Click_Quit()
    {
        this.gameObject.SetActive(false);
        PopMessageUI.ClearMessages();
        GameRoot._Root.sceneChanger.ChangeScene(SceneChanger.scenes.intro);
    }
}
