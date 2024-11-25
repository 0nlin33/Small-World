using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public Resources resourceType;

    public string resourceName;

    public int harvestQuantity;
    public int harvestRate;
    public int harvestTime;
    private System.Action onDepletedCallback;

    public Action<int> OnResourceHarvested;

    public void Initialize(System.Action onDepleted)
    {
        onDepletedCallback = onDepleted;
    }

    private void Start()
    {
        resourceName = resourceType.resourceName;
        harvestQuantity = resourceType.resourceAmount;
        harvestRate = resourceType.gatherRate;
        harvestTime = resourceType.gatherTime;
    }

    public int Harvest()
    {
        harvestQuantity = harvestQuantity - harvestRate;

        // Trigger visual and audio effects
        if (resourceType.harvestEffect)
        {
            Instantiate(resourceType.harvestEffect, transform.position, Quaternion.identity);
        }
        if (resourceType.harvestSound)
        {
            AudioSource.PlayClipAtPoint(resourceType.harvestSound, transform.position);
        }

        if (harvestQuantity <= 0)
        {
            onDepletedCallback?.Invoke();
            StartCoroutine(DestroyResource());
        }

        OnResourceHarvested?.Invoke(harvestRate);

        return harvestRate;
    }

    IEnumerator DestroyResource()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
