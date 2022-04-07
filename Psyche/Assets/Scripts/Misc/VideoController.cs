using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

public class VideoController : MonoBehaviour
{
    public GameObject videoPanel;
    private static RawImage videoScreen;
    private static VideoPlayer videoPlayer;
    public VideoHolder vidHolder;
    public static VideoClip clip;
    public Button btn_Skip;
    public Button btn_Pause;
    public string videoName;
    private bool pause = false;
    private bool skipping = false;
    public bool retarting = true;
    public float fadeSpeed = 0.4f;
    public Sprite playSprite;
    public Sprite pauseSprite;
    public UnityEvent videoEnd;

    // Start is called before the first frame update
    void Start()
    {
        videoScreen = videoPanel.GetComponent<RawImage>();
        videoPlayer = videoPanel.GetComponent<VideoPlayer>();
        videoPlayer.SetDirectAudioVolume(ushort.MinValue, 1.0f);
        vidHolder = GetComponentInChildren<VideoHolder>();
        vidHolder.clipSelect = 0;
        clip = vidHolder.videos[vidHolder.clipSelect];
        videoEnd = new UnityEvent();
        Debug.Log(Application.streamingAssetsPath);
    }

    // Update is called once per frame
    void Update()
    {
        if(skipping || videoPlayer.time > videoPlayer.length * 0.95f)
        {
            FadeOutVideo();
        }
        else if(retarting)
        {
            FadeInVideo();
        }
    }

    private void FadeOutVideo()
    {
        videoScreen.color -= new Color(0, 0, 0, Time.deltaTime * fadeSpeed);
        
        //there's a crash that happens in WebGL if the volume is negative
        if(videoScreen.color.a >= 0.001f)
            videoPlayer.SetDirectAudioVolume(ushort.MinValue, videoScreen.color.a);
        else
        {
            videoEnd.Invoke();   
            videoPlayer.Stop();
            this.gameObject.SetActive(false);
        }
    }

    private void FadeInVideo()
    {
        videoScreen.color += new Color(0, 0, 0, Time.deltaTime * fadeSpeed);
        
        if (videoScreen.color.a <= 0.99f)
            videoPlayer.SetDirectAudioVolume(ushort.MinValue, videoScreen.color.a);
        else
        {
            retarting = false;
            videoScreen.color = new Color(1, 1, 1, 1);
        }
    }

    public void StartVideo(string video)
    {
        videoName = video;

        Debug.Log(Application.streamingAssetsPath + videoName);
        if (videoScreen == null)
            videoScreen = videoPanel.GetComponent<RawImage>();
        if (videoPlayer == null)
            videoPlayer = videoPanel.GetComponent<VideoPlayer>();


        videoPlayer.clip = clip;

        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
        videoPlayer.waitForFirstFrame = true;
        videoPlayer.Prepare();
        videoPlayer.SetDirectAudioVolume(ushort.MinValue, 1.0f);
        skipping = false;
        retarting = false;
        videoScreen.color = new Color(1, 1, 1, 1);
        videoPlayer.frame = 0;
        videoPlayer.time = 0.0f;
        videoPlayer.Play();
        this.gameObject.SetActive(true);
        btn_Skip.gameObject.SetActive(true);
    }

    public void RestartVideo(string video)
    {
        videoName = video;

        videoPlayer.clip = clip;


        Debug.Log(Application.streamingAssetsPath + videoName);
        if (videoScreen == null)
            videoScreen = videoPanel.GetComponent<RawImage>();
        if (videoPlayer == null)
            videoPlayer = videoPanel.GetComponent<VideoPlayer>();

        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
        videoPlayer.waitForFirstFrame = true;
        videoPlayer.Prepare();
        videoPlayer.SetDirectAudioVolume(ushort.MinValue, 1.0f);

        skipping = false;
        retarting = true;
        videoScreen.color = new Color(1, 1, 1, 0);
        videoPlayer.frame = 0;
        videoPlayer.time = 0.0f;
        videoPlayer.Play();
        this.gameObject.SetActive(true);
        btn_Skip.gameObject.SetActive(true);
    }

    public void Click_Pause()
    {
        pause = !pause;
        if (pause)
        {
            btn_Pause.GetComponent<Image>().sprite = playSprite;
            videoPlayer.playbackSpeed = 0.0f;
        }
        else
        {
            btn_Pause.GetComponent<Image>().sprite = pauseSprite;
            videoPlayer.playbackSpeed = 1.0f;
        }
    }

    public void Click_Skip()
    {
        btn_Skip.gameObject.SetActive(false);
        skipping = true;
    }
}
