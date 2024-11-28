using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    [Header("Gameobject Refrences")]
    [SerializeField] private GameObject quitPanel;
    [SerializeField] private GameObject quitPopup;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            DisplayQuitPanel();
        }
    }

    public void AskQuit()
    {
        DisplayQuitPanel();
    }

    private void DisplayQuitPanel()
    {
        quitPanel.SetActive(true);
        quitPopup.transform.DOScaleY(1, 0.3f).SetEase(Ease.Linear).SetUpdate(true);
    }

    public void HideQuitPanel()
    {
        StartCoroutine(CloseQuitPanel());
    }

    IEnumerator CloseQuitPanel()
    {
        quitPopup.transform.DOScaleY(0, 0.3f).SetEase(Ease.Linear).SetUpdate(true);
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(0.35f);
        quitPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void CloseGame()
    {
        StartCoroutine(CloseQuitPanel());
        Application.Quit();
    }
}
