using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameManager is where most things pretaining to game happens and GameManager lets other scripts know what is happening
    //GameManager is also made singleton in most cases as pre need
    private static GameManager instance;

    //variables used to store and reference data and objects
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
    //aboveand below part are used to make the class into a singleton
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


        //Fetching previous data
        metalCountPlayer = PlayerPrefs.GetInt(PLAYER_RESOURCE_METAL,0);
        woodCountPlayer = PlayerPrefs.GetInt(PLAYER_RESOURCE_WOOD, 0);
        //Sendin data to store value
        playerResources.NewMetalAmount(storedMetalAmount);
        playerResources.NewWoodAmount(storedWoodAmount);
    }

    //Event to notify other classes if they are listening
    [Header("Action Events")]
    public Action OnGatheringStart;


    void Start()
    {
        //swaitching player state to idle
        SwitchPlayerState(PlayerState.Idle);
    }

    //Function used to switch playerstate and run corresponding script as needed.
    //this script takes two parameters, one player state and another ResourceNode
    //playerState is variable the caller sends to notify what state the player should be in
    //ResourceNode is the resourceNode the function is currently able to access
    //The parameter for ResourceNode is optional as it also accepts null vaue as not all playerStates need to access ResourceNodes
    public void SwitchPlayerState(PlayerState newPlayerState, ResourceNode currentResource = null)
    {

        switch (newPlayerState)
        {
            //switch playerstate to idle
            case PlayerState.Idle:
                Debug.Log("Current Player State Is Idle");
                break;

            //switch playerstate to walking
            case PlayerState.Walking:
                Debug.Log("Current Player State Is Walking");
                break;

            //switch playerstate to Gathering
            case PlayerState.Gathering:
                OnGatheringStart?.Invoke();
                break;
        }
    }

    

    //Fucntion available to otherclasses if they need to harvestmetal
    public void HarvestMetal(int harvestAmount)
    {
        metalCountPlayer += harvestAmount;

        playerResources.NewMetalAmount(metalCountPlayer);

        PlayerPrefs.SetInt(PLAYER_RESOURCE_METAL, metalCountPlayer);
    }

    //Fucntion available to otherclasses if they need to harvestwood
    public void HarvestWood(int harvestAmount)
    {
        woodCountPlayer += harvestAmount;

        playerResources.NewWoodAmount(woodCountPlayer);

        PlayerPrefs.SetInt(PLAYER_RESOURCE_WOOD, woodCountPlayer);
    }


}

//enum are like dataTypes, we create whatever dataType we need and we refer to them as we need them
public enum PlayerState
{
    Idle,
    Walking,
    Gathering
}
