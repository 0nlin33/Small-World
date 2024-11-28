using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //This is audiomanager singleton, responsible for notifying any script that needs if audio should be accessible or not
    //This allows to chanege the status if audio should be available or not and it allows others to know if what status audio is in
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

        //When game starts, check if audio is enabled or not
        audioStatus = PlayerPrefs.GetInt(AUDIO_STATUS, 0) == 1;
    }

    //variables to store things
    private string AUDIO_STATUS = "audioStatus";
    [SerializeField]private bool audioStatus = true;


    //give audiostatus information to other scripts
    public bool  GetAudioStatus()
    {
        return audioStatus;
    }

    //change audio status, public exposed
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
