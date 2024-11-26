using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInfoDisplay : MonoBehaviour
{
    [Header("UI Refrences")]
    [SerializeField] private TextMeshProUGUI metalCountDisplay;
    [SerializeField] private TextMeshProUGUI woodCountDisplay;
    [SerializeField] private TextMeshProUGUI updateQuest;
    [SerializeField] private Slider channelSlider;

    private float channelValue;


    void Start()
    {
        GameManager.Instance.OnMetalHarvest += IncreaseMetalCountDisplay;
        GameManager.Instance.OnWoodHarvest += IncreaseWoodCountDisplay;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnMetalHarvest -= IncreaseMetalCountDisplay;
        GameManager.Instance.OnWoodHarvest -= IncreaseWoodCountDisplay;
    }

    private void IncreaseMetalCountDisplay(int metalCount)
    {
        channelSlider.value = 0;
        metalCountDisplay.text = metalCount.ToString();
    }

    private void IncreaseWoodCountDisplay(int woodCount)
    {
        channelSlider.value = 0;
        woodCountDisplay.text = woodCount.ToString();
    }

    private void Update()
    {
        
    }

}
