using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    //GameScript is added to add life to the game with a bit of lore

    //made singleton
    public static GameScript instance;

    //variables and  refrences
    [Header("GameObject Refrences")]
    [SerializeField] GameObject sea;
    [SerializeField] GameObject gameUICanvas;
    [SerializeField] GameObject endGameCanvas;
    [SerializeField] GameObject startGameHodler;
    [SerializeField] GameObject endGameHolder;

    //creted a new list
    public List<GameObject> resources = new List<GameObject>();

    //fucntion to quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    //simple singleton
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

    //Add to list if object is not already in the list, from event
    public void AddObject(GameObject obj)
    {
        if (!resources.Contains(obj))
        {
            resources.Add(obj);
        }
    }
    
    //remove from the list if the object stops existing, from event
    public void RemoveObject(GameObject obj)
    {
        if (resources.Contains(obj))
        {
            resources.Remove(obj);
        }

        
    }
    
    bool drownWorld = false;

    //in update we check how many elements are in list and if there are no elements left on the list, the games moves towards ending
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
