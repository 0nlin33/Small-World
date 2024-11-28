using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public static GameScript instance;

    [Header("GameObject Refrences")]
    [SerializeField] GameObject sea;
    [SerializeField] GameObject gameUICanvas;
    [SerializeField] GameObject endGameCanvas;
    [SerializeField] GameObject startGameHodler;
    [SerializeField] GameObject endGameHolder;

    public List<GameObject> resources = new List<GameObject>();


    public void QuitGame()
    {
        Application.Quit();
    }


    private void Awake()
    {
        // Ensure this manager is a singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddObject(GameObject obj)
    {
        if (!resources.Contains(obj))
        {
            resources.Add(obj);
        }
    }

    public void RemoveObject(GameObject obj)
    {
        if (resources.Contains(obj))
        {
            resources.Remove(obj);
        }

        
    }
    bool drownWorld = false;

    private void Update()
    {
        if (resources.Count <= 0)
        {
            gameUICanvas.SetActive(false);
            endGameCanvas.SetActive(true);
            startGameHodler.SetActive(false);
            endGameHolder.SetActive(true);

            drownWorld = true;
        }

        if (drownWorld)
        {
            sea.transform.DOMoveY(3, 20).SetEase(Ease.Linear).SetUpdate(true);
        }
    }


}
