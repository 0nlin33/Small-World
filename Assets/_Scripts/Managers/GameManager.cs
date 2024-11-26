using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

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

    public ResourceNode currentResource;

    [SerializeField]PlayerResources playerResource;

    public Action<int> OnMetalHarvest;
    public Action<int> OnWoodHarvest;

    void Start()
    {
        SwitchPlayerState(PlayerState.Idle);
    }

    public void SwitchPlayerState(PlayerState newPlayerState)
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
                GatherHandling();
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Some collider has entered body"+other.name);

        if (other.CompareTag("Resource"))
        {
            currentResource = other.GetComponent<ResourceNode>();
            SwitchPlayerState(PlayerState.Gathering);
            Debug.Log("Entered resource: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            if (currentResource == other.GetComponent<ResourceNode>())
            {
                currentResource = null;
                SwitchPlayerState(PlayerState.Idle);
                Debug.Log("Exited resource: " + other.name);
            }
        }
    }


    public void GatherHandling()
    {
        StartCoroutine(Gathering());
    }

    IEnumerator Gathering()
    {
        bool isGathering = true;
        while(isGathering)
        {
            if(currentResource != null)
            {
                if (currentResource.harvestQuantity > 0)
                {
                    ContinueHarvesting();
                }
                else
                {
                    isGathering = false;
                    SwitchPlayerState(PlayerState.Idle);
                }
            }
            else
            {
                isGathering = false;
                SwitchPlayerState(PlayerState.Idle);
            }
            yield return new WaitForSeconds(0.5f);

        }
    }

    void ContinueHarvesting()
    {
        if(currentResource != null)
        {
            if (currentResource.resourceName == "MetalOre")
            {
                playerResource.metalAmount += currentResource.Harvest();

                int metalCount = playerResource.metalAmount;

                OnMetalHarvest?.Invoke(metalCount);
            }
            else if (currentResource.resourceName == "TreeLog")
            {
                playerResource.woodAmount += currentResource.Harvest();

                int woodCount = playerResource.woodAmount;

                OnWoodHarvest?.Invoke(woodCount);
            }
        }
        else
        {
            Debug.Log("Resource completely Depleted");
        }

        
    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Gathering
}
