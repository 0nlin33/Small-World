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
}
//Scriptable object that can be used to store data which is used by multiple objects/gameobjects in the game