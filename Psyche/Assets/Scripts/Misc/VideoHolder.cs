using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoHolder : MonoBehaviour
{
    public int clipSelect = 0;
    public VideoClip[] videos; 

    public void Start()
    {
        VideoController.clip = videos[clipSelect];
    }

    public void ChangeClip(int index)
    {
        clipSelect = index;
        VideoController.clip = videos[clipSelect];
    }

}
