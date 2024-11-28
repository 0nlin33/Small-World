using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUIHandler : MonoBehaviour
{
    //This script is responsible for handling UI part of audio, how we refrence which audio and if to play audio or not according to audioManager

    //Variables
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


    //subscribing to events to play audio at correct time
    private void Start()
    {
        PlayBackgroundMusic();

        playerHandler.OnResourceEnterMetal += PlayMetalAudio;
        playerHandler.OnResourceEnterWood += PlayWoodAudio;
        playerHandler.OnResourceDepleted += PlayTwinkleAudio;
    }

    //unsubscribing
    private void OnDisable()
    {
        playerHandler.OnResourceEnterMetal -= PlayMetalAudio;
        playerHandler.OnResourceEnterWood -= PlayWoodAudio;
        playerHandler.OnResourceDepleted -= PlayTwinkleAudio;
    }

    //letting player turn sound on or off
    public void ToggleAudio()
    {
        audioManager.ToggleAudioOption();
        PlayBackgroundMusic();
    }

    //playing backgroundmusic
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

    //play metal harvesting sound
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

    //play wood harvesting sound
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

    //play twinkle sound
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
