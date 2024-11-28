using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    [Header("Gameobject Refrences")]
    [SerializeField] private GameObject quitPanelHolder;
    [SerializeField] private GameObject quitPopup;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            DisplayQuitPanel();
        }
    }

    private void DisplayQuitPanel()
    {
        quitPanelHolder.SetActive(true);
        quitPopup.transform.DOScaleY(1, 0.3f).SetEase(Ease.Linear).SetUpdate(true);
    }

    public void HideQuitPanel()
    {
        StartCoroutine(CloseQuitPanel());
    }

    IEnumerator CloseQuitPanel()
    {
        quitPopup.transform.DOScaleY(0, 0.3f).SetEase(Ease.Linear).SetUpdate(true);
        yield return new WaitForSeconds(.35f);
        quitPanelHolder.SetActive(false);
    }

    public void CloseGame()
    {
        StartCoroutine(CloseQuitPanel());
        Application.Quit();
    }
}
