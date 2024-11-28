using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public Resources resourceType;
    public CursorChanger cursorChanger;

    private string RESOURCE_DEPLETION_METAL = "Harvested_Count_Metal";
    private string RESOURCE_DEPLETION_WOOD = "Harvested_Count_Wood";

    public int harvestedAmount;
    public int harvestQuantity = 0;


    public string resourceName;

    public int harvestQuantityMax;
    public int harvestRate;
    public int harvestTime;
    //private System.Action onDepletedCallback;

    public Action<int> OnResourceHarvested;

   /* public void Initialize(System.Action onDepleted)
    {
        onDepletedCallback = onDepleted;
    }*/



    private void Start()
    {
        resourceName = resourceType.resourceName;
        harvestQuantityMax = resourceType.resourceAmount;
        harvestRate = resourceType.gatherRate;
        harvestTime = resourceType.gatherTime;

        if(resourceName == "MetalOre")
        {
            harvestedAmount = PlayerPrefs.GetInt(RESOURCE_DEPLETION_METAL, 0);
        }
        else
        {
            harvestedAmount = PlayerPrefs.GetInt(RESOURCE_DEPLETION_WOOD, 0);
        }

        if (harvestedAmount > 0)
        {
            harvestQuantity = harvestQuantityMax - harvestedAmount;
        }
        else
        {
            harvestQuantity = harvestQuantityMax;

        }
    }

    private void OnMouseEnter()
    {
        cursorChanger.MineResource();
    }

    private void OnMouseExit()
    {
        cursorChanger.ClickGround();
    }

    public int Harvest()
    {
        harvestQuantity = harvestQuantity - harvestRate;

        if (harvestQuantity <= 0)
        {
            StartCoroutine(DestroyResource());
        }

        int increaseResourceDelpetion = 1;
        increaseResourceDelpetion = increaseResourceDelpetion + harvestRate;

        if(resourceName == "MetalOre")
        {
            PlayerPrefs.SetInt(RESOURCE_DEPLETION_METAL, increaseResourceDelpetion);
        }
        else
        {
            PlayerPrefs.SetInt(RESOURCE_DEPLETION_WOOD, increaseResourceDelpetion);
        }

        return harvestRate;
    }

    IEnumerator DestroyResource()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
