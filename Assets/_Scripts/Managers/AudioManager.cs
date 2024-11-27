using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            instance = FindObjectOfType<AudioManager>();
            if(instance == null)
            {
                instance = new GameObject().AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null && instance != this) 
        {
            Destroy(gameObject);
        }
        else
        {
            instance=this;
            DontDestroyOnLoad(gameObject);
        }

        audioStatus = PlayerPrefs.GetInt(AUDIO_STATUS, 0) == 1;
    }


    private string AUDIO_STATUS = "audioStatus";
    [SerializeField]private bool audioStatus;


    public bool  GetAudioStatus()
    {
        return audioStatus;
    }

    public void ToggleAudioOption()
    {
        ChangeAudioStatus();
    }


   private void ChangeAudioStatus()
   {
        audioStatus = !audioStatus;

        if(audioStatus)
        {
            PlayerPrefs.SetInt(AUDIO_STATUS, 1);
        }
        else
        {
            PlayerPrefs.SetInt(AUDIO_STATUS, 0);
        }
   }
}
