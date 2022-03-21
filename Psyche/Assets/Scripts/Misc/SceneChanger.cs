using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        GameRoot.resourceService.AsynLoadScene(sceneName, () =>
        {
          //  CloseAllWindow();
            switch (sceneName)
            {
                case Constants.Scenes.intro:

                    GameRoot._Root.audioService.PlayBgMusic(Constants.Audio.Menu.CosmicSwitchboard, true);
                    break;

                case Constants.Scenes.tutorial:
                    GameRoot._Root.audioService.PlayBgMusic(Constants.Audio.Menu.DystopicTechnology, true);
                    break;

                case Constants.Scenes.level1:
                    GameRoot._Root.audioService.PlayBgMusic(Constants.Audio.Gameplay.BlazingStars, true);
                    break;

                case Constants.Scenes.level2:
                    GameRoot._Root.audioService.PlayBgMusic(Constants.Audio.Gameplay.ColdMoon, true);
                    break;

                case Constants.Scenes.level3:
                    GameRoot._Root.audioService.PlayBgMusic(Constants.Audio.Gameplay.CyberREM, true);
                    break;

                case Constants.Scenes.level4:
                    GameRoot._Root.audioService.PlayBgMusic(Constants.Audio.Gameplay.RetroSciFiPlanet, true);
                    break;

                case Constants.Scenes.endOfLevel:
                    GameRoot._Root.audioService.PlayBgMusic(Constants.Audio.Menu.OminousSciFiMenu, true);
                    break;

                case Constants.Scenes.Credits:
                    GameRoot._Root.audioService.PlayBgMusic(Constants.Audio.Menu.CyberStreets, true);
                    break;
            }
        });
    }
}

