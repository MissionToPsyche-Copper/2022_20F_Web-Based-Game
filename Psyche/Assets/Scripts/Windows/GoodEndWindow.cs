using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodEndWindow : MonoBehaviour
{
    public Button btn_NextOrbit;
    public SceneChanger.scenes nextScene;

    public Transform scorePanel;
    public ScoresPanel scores;


    public void Click_NextOrbit()
    {
        this.gameObject.SetActive(false);
        SceneController.sceneRoot.OnSceneChange();
        GameRoot._Root.sceneChanger.ChangeScene(SceneChanger.scenes.endoflevel);
    }

    public void SetNextOrbit(SceneChanger.scenes scene)
    {
        nextScene = scene;
    }
}
