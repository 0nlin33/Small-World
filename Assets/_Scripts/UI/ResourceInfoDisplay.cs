using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInfoDisplay : MonoBehaviour
{
    private string PLAYER_RESOURCE_METAL = "Metal_Count";
    private string PLAYER_RESOURCE_WOOD = "WOOD_Count";

    [Header("UI Refrences")]
    [SerializeField] private TextMeshProUGUI metalCountDisplay;
    [SerializeField] private TextMeshProUGUI woodCountDisplay;
   /* [SerializeField] private TextMeshProUGUI updateQuest;
    [SerializeField] private Slider channelSlider;*/

    [Header("Player Refrence")]
    [SerializeField] private PlayerCollisionHandler playerCollision;

    private float channelValue;

    private void Awake()
    {
        MetalCountDisplay();
        WoodCountDisplay();
    }


    void Start()
    {
        playerCollision.OnMetalHarvest += MetalCountDisplay;
        playerCollision.OnWoodHarvest += WoodCountDisplay;
    }

    private void OnDestroy()
    {
        playerCollision.OnMetalHarvest -= MetalCountDisplay;
        playerCollision.OnWoodHarvest -= WoodCountDisplay;
    }

    private void MetalCountDisplay()
    {
        //channelSlider.value = 0;

        int metalCount = PlayerPrefs.GetInt(PLAYER_RESOURCE_METAL, 0);
        metalCountDisplay.text = metalCount.ToString();
    }

    private void WoodCountDisplay()
    {
        //channelSlider.value = 0;

        int woodCount = PlayerPrefs.GetInt(PLAYER_RESOURCE_WOOD, 0);
        woodCountDisplay.text = woodCount.ToString();
    }

    private void Update()
    {
        
    }

}
