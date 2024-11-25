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
    }


    private string AUDIO_STATUS = "audioStatus";
    private bool audioStatus;

    [Header("Audio Source Refrences")]
    [SerializeField] AudioSource metalAudio;
    [SerializeField] AudioSource woodAudio;
    [SerializeField] AudioSource backgroundMusic;


    // Start is called before the first frame update
    void Start()
    {
        audioStatus = PlayerPrefs.GetInt(AUDIO_STATUS, 0) == 1;
        AudioStatusReflect();
    }

    void AudioStatusReflect()
    {
        if (audioStatus)
        {
            metalAudio.enabled = true;
            woodAudio.enabled = true;
            backgroundMusic.enabled = true;
        }
        else
        {
            metalAudio.enabled = false;
            woodAudio.enabled = false;
            backgroundMusic.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
