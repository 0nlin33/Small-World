using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickParticlesPool : MonoBehaviour
{

    private static ClickParticlesPool instance;

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

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int poolCount = 5;

    [SerializeField] private GameObject clickEffectPrefab;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolCount; i++)
        {
            GameObject obj = Instantiate(clickEffectPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

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
