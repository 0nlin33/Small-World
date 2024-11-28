using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Some collider has entered body" + other.name);

        if (other.CompareTag("Resource"))
        {
            OnResourceEnter?.Invoke(true);
            currentResource = other.GetComponent<ResourceNode>();

            if (currentResource.resourceName == "MetalOre")
            {
                OnResourceEnterMetal?.Invoke(true);
            }
            else if(currentResource.resourceName == "TreeLog")
            {
                OnResourceEnterWood?.Invoke(true);
            }

            GameManager.Instance.SwitchPlayerState(PlayerState.Gathering, currentResource);
            Debug.Log("Entered resource: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Resource"))
        {
            OnResourceEnter?.Invoke(false);
            OnResourceEnterMetal?.Invoke(false);
            OnResourceEnterWood?.Invoke(false);

            if (currentResource == other.GetComponent<ResourceNode>())
            {
                currentResource = null;
                GameManager.Instance.SwitchPlayerState(PlayerState.Idle);
                Debug.Log("Exited resource: " + other.name);
            }
        }
    }


    public void GatheringStart()
    {
        StartCoroutine(Gathering());
    }

    public Action<bool> OnStartChanneling;
    IEnumerator Gathering()
    {
        bool isGathering = true;
        bool startChanneling = true;

        while (isGathering)
        {
            OnStartChanneling?.Invoke(true);
            yield return new WaitForSeconds(2f);

            if (currentResource != null)
            {
                if (currentResource.harvestQuantity > 0)
                {
                    startChanneling = true ;
                    ContinueHarvesting(currentResource);
                }
                else
                {
                    OnResourceDepleted?.Invoke(true);
                    startChanneling = false;
                    isGathering = false;
                    gameManager.SwitchPlayerState(PlayerState.Idle, null);
                }
            }
            else
            {
                startChanneling = false;
                isGathering = false;
                gameManager.SwitchPlayerState(PlayerState.Idle, null);
            }
        }
    }

    void ContinueHarvesting(ResourceNode currentResource)
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
