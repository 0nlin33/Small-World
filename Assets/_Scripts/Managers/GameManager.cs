using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private string PLAYER_RESOURCE_METAL = "Metal_Count";
    private string PLAYER_RESOURCE_WOOD = "WOOD_Count";

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
    }


    [SerializeField]PlayerResources playerResource;

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
        playerResource.metalAmount += harvestAmount;
        PlayerPrefs.SetInt(PLAYER_RESOURCE_METAL, playerResource.metalAmount);
    }

    public void HarvestWood(int harvestAmount)
    {
        playerResource.woodAmount += harvestAmount;
        PlayerPrefs.SetInt(PLAYER_RESOURCE_WOOD, playerResource.woodAmount);
    }


}

public enum PlayerState
{
    Idle,
    Walking,
    Gathering
}
