﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceService : MonoBehaviour
{
    public void InitService()
    {
        Debug.Log("Init Service");
    }

    private void Update()
    {
        //Update the loading acton
        if (percentageCB != null)
        {
            percentageCB();
        }
    }

    private Action percentageCB = null;
    public void AsynLoadScene(string sceneName, Action loaded)
    {
        //Async load the scene
        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);

        //Active the loading window
        GameRoot._Root.windowsController.loadingWindow.gameObject.SetActive(true);

        //Action needs to be update during loading
        percentageCB = () =>
        {
            float percentage = sceneAsync.progress;
            //         GameRoot3D.instance.loadingWindow.SetProgress(percentage);
            GameRoot._Root.windowsController.loadingWindow.SetProgress(percentage);
            //Async loading is finished
            if (percentage >= 0.90f)
            {
                //If there is a window need to be actived, do the action
                if (loaded != null)
                {
                    loaded();
                }

                //Inactive the loading window and clean relative values;
                GameRoot._Root.windowsController.loadingWindow.gameObject.SetActive(false);
                sceneAsync = null;
                percentageCB = null;
            }
        };
    }

    private Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path, bool isCache)
    {
        AudioClip audioClip = null;

        //If the audio dictionary doesn't cache the audio
        if (!audioDictionary.TryGetValue(path, out audioClip))
        {
            //Load the audio
            audioClip = Resources.Load<AudioClip>(path);
            //Check if the audio need to be cached
            if (isCache)
            {
                audioDictionary.Add(path, audioClip);
            }
        }
        return audioClip;
    }
}
