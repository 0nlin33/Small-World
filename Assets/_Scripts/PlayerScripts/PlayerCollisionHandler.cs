using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("Refrences")]
    public ResourceNode currentResource;

    [Header("Refrences")]
    public Action OnMetalHarvest;
    public Action OnWoodHarvest;


    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        gameManager.OnGatheringStart+= GatheringStart;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Some collider has entered body" + other.name);

        if (other.CompareTag("Resource"))
        {
            currentResource = other.GetComponent<ResourceNode>();
            GameManager.Instance.SwitchPlayerState(PlayerState.Gathering, currentResource);
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
                GameManager.Instance.SwitchPlayerState(PlayerState.Idle);
                Debug.Log("Exited resource: " + other.name);
            }
        }
    }


    public void GatheringStart()
    {
        StartCoroutine(Gathering());
    }

    IEnumerator Gathering()
    {
        bool isGathering = true;
        while (isGathering)
        {
            if (currentResource != null)
            {
                if (currentResource.harvestQuantity > 0)
                {
                    ContinueHarvesting(currentResource);
                }
                else
                {
                    isGathering = false;
                    gameManager.SwitchPlayerState(PlayerState.Idle, null);
                }
            }
            else
            {
                isGathering = false;
                gameManager.SwitchPlayerState(PlayerState.Idle, null);
            }
            yield return new WaitForSeconds(0.5f);

        }
    }

    void ContinueHarvesting(ResourceNode currentResource)
    {
        if (currentResource != null)
        {
            if (currentResource.resourceName == "MetalOre")
            {
                gameManager.HarvestMetal(currentResource.Harvest());

                OnMetalHarvest?.Invoke();
            }
            else if (currentResource.resourceName == "TreeLog")
            {
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
