using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private string PLAYER_RESOURCE_METAL = "Metal_Count";
    private string PLAYER_RESOURCE_WOOD = "Wood_Count";

    private int storedMetalAmount;
    private int storedWoodAmount;

    int metalCountPlayer;
    int woodCountPlayer;

    public PlayerResources playerResources = new PlayerResources();

    public static GameManager Instance
    {
        get
        {
            instance = FindObjectOfType<GameManager>();
            if(instance == null)
            {
                instance = new GameObject().AddComponent<GameManager>();
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
            instance = this;
            DontDestroyOnLoad(this);
        }


        metalCountPlayer = PlayerPrefs.GetInt(PLAYER_RESOURCE_METAL,0);
        woodCountPlayer = PlayerPrefs.GetInt(PLAYER_RESOURCE_WOOD, 0);

        playerResources.NewMetalAmount(storedMetalAmount);
        playerResources.NewWoodAmount(storedWoodAmount);
    }

    [Header("Action Events")]
    public Action OnGatheringStart;


    void Start()
    {
        SwitchPlayerState(PlayerState.Idle);
    }

    public void SwitchPlayerState(PlayerState newPlayerState, ResourceNode currentResource = null)
    {

        switch (newPlayerState)
        {
            case PlayerState.Idle:
                Debug.Log("Current Player State Is Idle");
                break;

            case PlayerState.Walking:
                Debug.Log("Current Player State Is Walking");
                break;

            case PlayerState.Gathering:
                OnGatheringStart?.Invoke();
                break;
        }
    }

    


    public void HarvestMetal(int harvestAmount)
    {
        metalCountPlayer += harvestAmount;

        playerResources.NewMetalAmount(metalCountPlayer);

        PlayerPrefs.SetInt(PLAYER_RESOURCE_METAL, metalCountPlayer);
    }

    public void HarvestWood(int harvestAmount)
    {
        woodCountPlayer += harvestAmount;

        playerResources.NewWoodAmount(woodCountPlayer);

        PlayerPrefs.SetInt(PLAYER_RESOURCE_WOOD, woodCountPlayer);
    }


}

public enum PlayerState
{
    Idle,
    Walking,
    Gathering
}
