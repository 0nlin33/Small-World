using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    //Refrencing scriptable object to get data in said scriptable object and use them for the gameobject with this script
    public Resources resourceType;

    //Refrencing CursorChanger class to call it's public function/method
    public CursorChanger cursorChanger;

    //constants stored in string to reduce error chances
    private string RESOURCE_DEPLETION_METAL = "Harvested_Count_Metal";
    private string RESOURCE_DEPLETION_WOOD = "Harvested_Count_Wood";

    //Below this are only variable used to store value obtained by various means, mostly from the scriptable object
    public int harvestedAmount;
    public int harvestQuantity = 0;


    public string resourceName;

    public int harvestQuantityMax;
    public int harvestRate;
    public int harvestTime;

    //Action is used to 
    public Action<int> OnResourceHarvested;


    private void Awake()
    {
        GameScript.instance.AddObject(gameObject);
    }


    private void Start()
    {
        //getting data from scriptableobject and storig them in variables to use in this script where needed
        resourceName = resourceType.resourceName;
        harvestQuantityMax = resourceType.resourceAmount;
        harvestRate = resourceType.gatherRate;
        harvestTime = resourceType.gatherTime;

        //Checking which scriptable object is being accessed in order to send data to correct reciever
        if(resourceName == "MetalOre")
        {
            harvestedAmount = PlayerPrefs.GetInt(RESOURCE_DEPLETION_METAL, 0);
        }
        else
        {
            harvestedAmount = PlayerPrefs.GetInt(RESOURCE_DEPLETION_WOOD, 0);
        }


        //Checking how much has been harvested in previous session or if none has been harvested
        if (harvestedAmount > 0)
        {
            harvestQuantity = harvestQuantityMax - harvestedAmount;
        }
        else
        {
            harvestQuantity = harvestQuantityMax;

        }
    }


    //Checking if mouse has entered the collider for the gameobject holding this script
    private void OnMouseEnter()
    {
        cursorChanger.MineResource();
    }

    //Checking if mouse has exited the collider for the gameobject holding this script
    private void OnMouseExit()
    {
        cursorChanger.ClickGround();
    }

    //Function to reduce the value of the the amount left on the resource to be harvested and how much is being harvested each time
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

    //This coroutine is called when there is no amount left to be harvested in the resource
    IEnumerator DestroyResource()
    {
        yield return null;
        //This destroys and removes the gameobject holding this script from the game scene, hierarchy and memory
        Destroy(gameObject);
    }

    //This fucntion gets called when the gameobject holding this script gets destroyed from the scene.
    private void OnDestroy()
    {
        GameScript.instance.RemoveObject(gameObject);
    }
}
