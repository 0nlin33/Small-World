using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    //This script handles collision of the player gameobject with other things in scene
    //if favourable, this script takes refrence to the scripts from those other gameobjects, like resourceNodes to harvest resource

    [Header("Refrences")]
    public ResourceNode currentResource;

    [Header("Actions")]
    public Action OnMetalHarvest;
    public Action OnWoodHarvest;
    public Action<bool> OnResourceEnter;
    public Action<bool> OnResourceEnterMetal;
    public Action<bool> OnResourceEnterWood;
    public Action<bool> OnResourceDepleted;


    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        gameManager.OnGatheringStart += GatheringStart;
    }

    private void OnDisable()
    {
        gameManager.OnGatheringStart -= GatheringStart;
    }

    //This function gets called when the collider of this scripts gameobject enters another gameobjects collider in the scene
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Some collider has entered body" + other.name);

        if (other.CompareTag("Resource"))//checking if the encountered gameobject is resoure or not through tag
        {
            OnResourceEnter?.Invoke(true);
            currentResource = other.GetComponent<ResourceNode>();

            if (currentResource.resourceName == "MetalOre")//if it is resource, checking which one it is
            {
                OnResourceEnterMetal?.Invoke(true);
            }
            else if(currentResource.resourceName == "TreeLog")
            {
                OnResourceEnterWood?.Invoke(true);
            }

            GameManager.Instance.SwitchPlayerState(PlayerState.Gathering, currentResource);//changing state to gathering and sending refrence to Resource node to gameobject
            Debug.Log("Entered resource: " + other.name);
        }
    }

    //Gets called when the gameobject collider and other object collider stop being in contanct
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Resource"))//checking if exiting resource
        {
            OnResourceEnter?.Invoke(false);
            OnResourceEnterMetal?.Invoke(false);
            OnResourceEnterWood?.Invoke(false);

            if (currentResource == other.GetComponent<ResourceNode>())
            {
                currentResource = null;
                GameManager.Instance.SwitchPlayerState(PlayerState.Idle);//if exiting resource, make resource node null and change state to idle
                Debug.Log("Exited resource: " + other.name);
            }
        }
    }

    //action listenning to gamemanager if it should gather and when it should
    public void GatheringStart()
    {
        StartCoroutine(Gathering());
    }

    public Action<bool> OnStartChanneling;
    IEnumerator Gathering() //coroutine responsible for gathering resources
    {
        bool isGathering = true;
        bool startChanneling = true;

        while (isGathering)//while loop to check status and gather resource only in specified time duration
        {
            OnStartChanneling?.Invoke(true);
            yield return new WaitForSeconds(2f);

            if (currentResource != null)//execute following if resourcenode is not null
            {
                if (currentResource.harvestQuantity > 0) //execute following if resourcenode has resource to harvest more than 0
                {
                    startChanneling = true ;
                    ContinueHarvesting(currentResource); //send currentResource and resourceNode to ContinueHarvesting
                }
                else //execute following if resourcenode has  no resource to harvest
                {
                    OnResourceDepleted?.Invoke(true);
                    startChanneling = false;
                    isGathering = false;
                    gameManager.SwitchPlayerState(PlayerState.Idle, null);//change stat to null, resourcenode is null
                }
            }
            else
            {
                startChanneling = false;
                isGathering = false;
                gameManager.SwitchPlayerState(PlayerState.Idle, null);//if current resource is null, change state to idle
            }
        }
    }

    void ContinueHarvesting(ResourceNode currentResource) //this fucntion harvests resources with help of gameManager by accessing its methods
    {
        if (currentResource != null)
        {
            if (currentResource.resourceName == "MetalOre")
            {
                OnResourceEnterMetal?.Invoke(true);
               
                gameManager.HarvestMetal(currentResource.Harvest());

                OnMetalHarvest?.Invoke();
            }
            else if (currentResource.resourceName == "TreeLog")
            {

                OnResourceEnterWood?.Invoke(true);
                gameManager.HarvestWood(currentResource.Harvest());

                OnWoodHarvest?.Invoke();
            }
        }
        else
        {
            Debug.Log("Resource completely Depleted");
        }


    }
}
