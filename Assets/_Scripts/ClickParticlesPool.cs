using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickParticlesPool : MonoBehaviour
{
    //This part is creating a static variable called instance of this class
    private static ClickParticlesPool instance;

    //This part is checking if the scene has any object of the classname if not it is adding in a new class, getter,setter
    public static ClickParticlesPool Instance
    {
        get
        {
            instance = FindObjectOfType<ClickParticlesPool>();
            if(instance == null)
            {
                instance = new GameObject().AddComponent<ClickParticlesPool>();
            }
            return instance;
        }
    }

    //This part is making sure there is no more than one class of this type in the scene
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            //this part is making sure that if scene changes the obejct with this class doesnt get destroyed
            DontDestroyOnLoad(this);
        }
    }

    //Storing pooledObjects in list
    //Objects are stored in a pool so that we can reduce garbage collection
    //if we have to constantly use the objects by instantiating and removing them from the scene
    private List<GameObject> pooledObjects = new List<GameObject>();
    private int poolCount = 5;
    //total count of the pooled objects

    [SerializeField] private GameObject clickEffectPrefab;


    void Start()
    {
        //basic forloop instantiating andd adding gameobjects to the list created above
        for(int i = 0; i < poolCount; i++)
        {
            GameObject obj = Instantiate(clickEffectPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    //Function to give the first available object stored in the pool list when the function is called.
   public GameObject GetPooledObject()
    {
        for (int i = 0;i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
