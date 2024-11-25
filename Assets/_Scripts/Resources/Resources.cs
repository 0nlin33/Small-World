using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName =  "Create Resource")]
public class Resources : ScriptableObject
{
    public string resourceName;

    public int resourceAmount;

    public GameObject resourcePrefab;

    public int gatherTime;
    public int gatherRate;

    public GameObject harvestEffect;
    public AudioClip harvestSound;
}
