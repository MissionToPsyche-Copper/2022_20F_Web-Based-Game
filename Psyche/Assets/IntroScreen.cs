using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroScreen : MonoBehaviour
{
    public GameObject videoPanel;
    private RawImage videoScreen;
    private VideoPlayer videoPlayer;
    public Button btn_Skip;
    private bool skipping = false;
    private bool retarting = false;
    private float fadeSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        videoScreen = videoPanel.GetComponent<RawImage>();
        videoPlayer = videoPanel.GetComponent<VideoPlayer>();
        videoPlayer.SetDirectAudioVolume(ushort.MinValue, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(skipping || videoPlayer.time > videoPlayer.length * 0.95f)
        {
            EndIntro();
        }
        else if(retarting)
        {
            RestartIntro();
        }

    }

    private void EndIntro()
    {
        videoScreen.color -= new Color(0, 0, 0, Time.deltaTime * fadeSpeed);
        videoPlayer.SetDirectAudioVolume(ushort.MinValue, videoScreen.color.a);
        if (videoScreen.color.a <= 0.001f)
        {
            videoPlayer.Stop();
            this.gameObject.SetActive(false);
        }
    }

    private void RestartIntro()
    {
        videoScreen.color += new Color(0, 0, 0, Time.deltaTime * fadeSpeed);
        videoPlayer.SetDirectAudioVolume(ushort.MinValue, videoScreen.color.a);
        if (videoScreen.color.a >= 0.99f)
        {
            retarting = false;
            videoScreen.color = new Color(1, 1, 1, 1);
        }
    }

    public void ReplayIntro()
    {
        skipping = false;
        retarting = true;
        videoScreen.color = new Color(1, 1, 1, 0);
        videoPlayer.frame = 0;
        videoPlayer.time = 0.0f;        
        videoPlayer.Play();
        this.gameObject.SetActive(true);
        btn_Skip.gameObject.SetActive(true);
    }

    public void Click_Skip()
    {
        btn_Skip.gameObject.SetActive(false);
        skipping = true;
    }
}
