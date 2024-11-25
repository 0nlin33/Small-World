using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerResources playerResource;

    // Start is called before the first frame update
    void Start()
    {
        SwitchPlayerState(PlayerState.Idle, null);
    }

    public void SwitchPlayerState(PlayerState newPlayerState, ResourceNode resource)
    {
        switch (newPlayerState)
        {
            case PlayerState.Idle:
                break;

            case PlayerState.Walking:
                break;

            case PlayerState.Gathering:
                GatherHandling(resource);
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Resource"))
        {
            ResourceNode resource = other.GetComponent<ResourceNode>();
            SwitchPlayerState(PlayerState.Gathering, resource);
        }
    }

    public void GatherHandling(ResourceNode resource)
    {

        StartCoroutine(Gathering(resource));
    }

    IEnumerator Gathering(ResourceNode resource)
    {
        bool isGathering = true;
        while(isGathering)
        {
            if (resource.harvestQuantity > 0)
            {
                ContinueHarvesting(resource);
            }
            else
            {
                isGathering = false;
            }
            yield return new WaitForSeconds(2);
        }
    }

    void ContinueHarvesting(ResourceNode resource)
    {
        if (resource.resourceName == "MetalOre")
        {
            playerResource.metalAmount = resource.Harvest();
        }
        else if (resource.resourceName == "TreeLog")
        {
            playerResource.woodAmount = resource.Harvest();
        }
    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Gathering
}
