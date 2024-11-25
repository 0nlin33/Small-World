using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateResources : MonoBehaviour
{
    public Resources metalOre;
    public Resources treeLog;


    public List<Resources> resourceObjectPool = new List<Resources>();

    public int resourceCountOnScene = 3;

    private Vector3 PositionRandomizer()
    {
        int randomGenX = Random.Range(-3, 3);
        int randomGenz = Random.Range(-3, 3);

        Vector3 spawnPosition = new Vector3(randomGenX,0.8f, randomGenz);

        return spawnPosition;
    }

    
    void Start()
    {
        for(int i = 0; i < resourceCountOnScene; i++)
        {
            Instantiate(metalOre.resourcePrefab, PositionRandomizer(), Quaternion.identity);
            Instantiate(treeLog.resourcePrefab, PositionRandomizer(), Quaternion.identity);

            //

            //Resources newResource = Instantiate(resourceTypes[randomGen]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
