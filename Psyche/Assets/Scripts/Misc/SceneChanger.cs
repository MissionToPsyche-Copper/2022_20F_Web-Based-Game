using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    public enum scenes {
      none,
      intro,
      tutorial,
      level1,
      level2,
      level3,
      level4,
      endoflevel,
      credits
    };


    public void ChangeScene(scenes scene)
    {
        GameRoot.resourceService.LoadScene(TranslateSceneEnum(scene));
        switch (scene)
        {
            case scenes.none:
                break;
            case scenes.intro:
                GameRoot._Root.OnSceneLoad(8);

                break;
            case scenes.tutorial:
                GameRoot._Root.OnSceneLoad(8);

                break;

            case scenes.endoflevel:
                GameRoot._Root.OnSceneLoad(9);

                break;
            case scenes.credits:
                GameRoot._Root.OnSceneLoad(0);

                break;
            default:
                break;
        }

        //GameRoot.resourceService.AsynLoadScene(TranslateSceneEnum(scene), () =>
        //{
        //  //  CloseAllWindow();
        //});
    }

    public static scenes TranslateScenePath(string path)
    {
        switch (path)
        {
            case Constants.Scenes.intro:
                return scenes.intro;
            case Constants.Scenes.tutorial:
                return scenes.tutorial;
            case Constants.Scenes.level1:
                return scenes.level1;
            case Constants.Scenes.level2:
                return scenes.level2;
            case Constants.Scenes.level3:
                return scenes.level3;
            case Constants.Scenes.level4:
                return scenes.level4;
            case Constants.Scenes.endOfLevel:
                return scenes.endoflevel;
            case Constants.Scenes.Credits:
                return scenes.credits;
            default:
                return scenes.none;
        }
    }

    public static string TranslateSceneEnum(scenes scene)
    {
        switch(scene)
        {
            case scenes.none:
                return "";
            case scenes.intro:
                return Constants.Scenes.intro;
            case scenes.tutorial:
                return Constants.Scenes.tutorial;
            case scenes.level1:
                return Constants.Scenes.level1;
            case scenes.level2:
                return Constants.Scenes.level2;
            case scenes.level3:
                return Constants.Scenes.level3;
            case scenes.level4:
                return Constants.Scenes.level4;
            case scenes.endoflevel:
                return Constants.Scenes.endOfLevel;
            case scenes.credits:
                return Constants.Scenes.Credits;
            default:
                return "";

        }
    }
}

