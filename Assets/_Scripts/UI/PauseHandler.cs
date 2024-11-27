using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseHandler : MonoBehaviour
{
    [Header("Pause Menu Refrences")]
    [SerializeField] GameObject pausePanelHolder;
    [SerializeField] GameObject pauseMenu;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        DisplayPausePanel();
    }

    private void DisplayPausePanel()
    {
        pausePanelHolder.SetActive(true);

        pauseMenu.transform.DOScale(new Vector3(1, 1, 1), .3f).SetEase(Ease.Linear).SetUpdate(true);
    }

    private void ClosePauseMenu()
    {
        pauseMenu.transform.DOScale(new Vector3(1, 0, 1), .3f).SetEase(Ease.Linear).SetUpdate(true);
    }

    public void Unpause()
    {
        ClosePauseMenu();

        StartCoroutine(ResumeGame());
    }

    IEnumerator ResumeGame()
    {
        Time.timeScale = 1f;

        Debug.Log("Resuming game shortly");
        yield return new WaitForSeconds(0.35f);

        Debug.Log("Game should be resumed now");

        pausePanelHolder.SetActive(false);
    }
}
