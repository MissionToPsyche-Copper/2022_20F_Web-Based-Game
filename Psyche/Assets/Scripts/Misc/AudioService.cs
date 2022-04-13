using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioService : MonoBehaviour
{
    public AudioSource audioBg;
    public AudioSource audioUI;
    public AudioSource audioFX;
    private AudioLibrary library;
    public float MasterVolume = 1.0f;
    public float bgVolume = 0.5f;
    public float UIVolume;
    public float FXVolume;

    public void Awake()
    {
        library = this.GetComponent<AudioLibrary>();
    }

    public void Start()
    {
        MasterVolume = Constants.Audio.masterVolume;
        bgVolume = Constants.Audio.bgmVolume * Constants.Audio.masterVolume;
        UIVolume = Constants.Audio.uiVolume * Constants.Audio.masterVolume;
        FXVolume = Constants.Audio.fxVolume * Constants.Audio.masterVolume;
    }

    public void PlayBgMusic(string audioName, bool isLoop)
    {
        //Get the audio clip by the resource service
        AudioClip audioClip = GameRoot.resourceService.LoadAudio(audioName, true);

        audioClip = library.BGMS[0];

        //Check if the background audio clip whether is null or next audio clip name matches the current audio clip
        if (audioBg.clip == null || audioBg.clip.name != audioClip.name)
        {
            audioBg.clip = audioClip;
            audioBg.loop = isLoop;
            audioBg.Play();
            if (GameRoot._Root.isPause)
                audioBg.volume = bgVolume * MasterVolume * 0.5f;
            else
                audioBg.volume = bgVolume * MasterVolume;
        }
    }

    public void PlayBgMusic(AudioClip audio, bool isLoop)
    {
        //Get the audio clip by the resource service
        AudioClip audioClip = audio;

        //Check if the background audio clip whether is null or next audio clip name matches the current audio clip
        if (audioBg.clip == null || audioBg.clip.name != audioClip.name)
        {
            audioBg.clip = audioClip;
            audioBg.loop = isLoop;
            audioBg.Play();
            if (GameRoot._Root.isPause)
                audioBg.volume = bgVolume * MasterVolume * 0.5f;
            else
                audioBg.volume = bgVolume * MasterVolume;
        }
    }

    public void PlayUIAudio(string audioName)
    {
        //Get the audio clip by the resource service
        AudioClip audioClip = GameRoot.resourceService.LoadAudio(audioName, true);
        audioUI.clip = audioClip;
        audioUI.Play();
    }
    public void PlayUIAudio(AudioClip audio)
    {
        //Get the audio clip by the resource service
        audioUI.clip = audio;
        audioUI.loop = false;
        audioUI.Play();
    }

    public void PlayFXAudio(string audioName)
    {
        //Get the audio clip by the resource service
        AudioClip audioClip = GameRoot.resourceService.LoadAudio(audioName, true);
        audioFX.clip = audioClip;
        audioFX.Play();
    }

    public AudioClip GetFXAudioClip(string audioName)
    {
        return GameRoot.resourceService.LoadAudio(audioName, false);
    }

    public void PauseAllAudios()
    {
        audioBg.volume *= 0.2f;
    }

    public void ResumeAllAudios()
    {
        audioBg.volume *= 5f;
    }

    public void SetBgVolume(float volume)
    {
        bgVolume = volume;
        if(GameRoot._Root.isPause)
        {
            audioBg.volume = bgVolume * 0.5f;
        }
        else
        {
            audioBg.volume = bgVolume;
        }
    }

    public void SetSoundFXVolume(float volume)
    {
        UIVolume = volume * Constants.Audio.masterVolume;
        FXVolume = volume * Constants.Audio.masterVolume;
        audioUI.volume = UIVolume;
        audioFX.volume = FXVolume;
    }
}
