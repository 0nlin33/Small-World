using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUIHandler : MonoBehaviour
{
    private AudioManager audioManager;

    [SerializeField] private PlayerCollisionHandler playerHandler;

    [Header("Audio Clip Refrences")]
    [SerializeField] AudioClip metalAudio;
    [SerializeField] AudioClip woodAudio;
    [SerializeField] AudioClip twinkleAudio;

    [Header("Audio Clip Refrences")]
    [SerializeField] AudioSource soundEffectSource;
    [SerializeField] AudioSource backgroundMusic;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
    }

    private void Start()
    {
        PlayBackgroundMusic();

        playerHandler.OnResourceEnterMetal += PlayMetalAudio;
        playerHandler.OnResourceEnterWood += PlayWoodAudio;
        playerHandler.OnResourceDepleted += PlayTwinkleAudio;
    }

    private void OnDisable()
    {
        playerHandler.OnResourceEnterMetal -= PlayMetalAudio;
        playerHandler.OnResourceEnterWood -= PlayWoodAudio;
        playerHandler.OnResourceDepleted -= PlayTwinkleAudio;
    }

    public void ToggleAudio()
    {
        audioManager.ToggleAudioOption();
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        if (audioManager.GetAudioStatus())
        {
            backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Stop();
        }
    }

    public void PlayMetalAudio(bool playAudio)
    {
        if(playAudio)
        {
            bool audioON = audioManager.GetAudioStatus();

            if (audioON)
            {
                soundEffectSource.clip = metalAudio;
                soundEffectSource.Play();
            }
            else
            {
                soundEffectSource.clip = null;
                soundEffectSource.Stop();
            }
        }
        else
        {
            soundEffectSource.clip = null;
            soundEffectSource.Stop();
        }
        
    }

    public void PlayWoodAudio(bool playAudio)
    {
        if (playAudio)
        {
            bool audioON = audioManager.GetAudioStatus();

            if (audioON)
            {
                soundEffectSource.clip = woodAudio;
                soundEffectSource.Play();
            }
            else
            {
                soundEffectSource.clip = null;
                soundEffectSource.Stop();
            }
        }
        else
        {
            soundEffectSource.clip = null;
            soundEffectSource.Stop();
        }
        
    }

    public void PlayTwinkleAudio(bool playAudio)
    {
        if (playAudio)
        {
            bool audioON = audioManager.GetAudioStatus();

            if (audioON)
            {
                soundEffectSource.clip = twinkleAudio;
                soundEffectSource.Play();
            }
            else
            {
                soundEffectSource.clip = null;
                soundEffectSource.Stop();
            }
        }
        else
        {
            soundEffectSource.clip = null;
            soundEffectSource.Stop();
        }
        
    }
}
