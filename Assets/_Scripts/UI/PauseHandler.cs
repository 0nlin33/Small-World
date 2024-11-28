using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseHandler : MonoBehaviour
{
    //This script is responsible for handling pausing the game and resuming it, also, it has a few additional functionalities as it is also controlling UI

    //variables and refrences
    [Header("Pause Menu Refrences")]
    [SerializeField] GameObject pausePanelHolder;
    [SerializeField] GameObject pauseMenu;

    //fucntion called to pause game
    public void PauseGame()
    {
        Time.timeScale = 0f;//Makes the time in game to 0, functionally freezing the game in time
        DisplayPausePanel();
    }

    //this fucntion displays Pausepanel and the pause menu with a smooth animation
    private void DisplayPausePanel()
    {
        pausePanelHolder.SetActive(true);

        pauseMenu.transform.DOScale(new Vector3(1, 1, 1), .3f).SetEase(Ease.Linear).SetUpdate(true);
    }

    //this fucntion Closes the pause menu with a smooth animation
    private void ClosePauseMenu()
    {
        pauseMenu.transform.DOScale(new Vector3(1, 0, 1), .3f).SetEase(Ease.Linear).SetUpdate(true);
    }

    //this fucntion is called to unpause the game
    public void Unpause()
    {
        ClosePauseMenu();

        StartCoroutine(ResumeGame());
    }

    //this coroutine sets the time of game back to 1 and unfreezes the games
    IEnumerator ResumeGame()
    {
        Time.timeScale = 1f;

        Debug.Log("Resuming game shortly");
        yield return new WaitForSeconds(0.35f);

        Debug.Log("Game should be resumed now");

        pausePanelHolder.SetActive(false);
    }
}
