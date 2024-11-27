using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelUIScript : MonoBehaviour
{

    public Slider gatherSlider;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGatheringStart += StartChanneling;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGatheringStart -= StartChanneling;
    }

    private void StartChanneling()
    {
        gatherSlider.value = 0;

        float channelDuration = 2;

        UpdateSliderValue(channelDuration);
    }

    void UpdateSliderValue(float channelDuration)
    {
        bool sliderMax = true;

        float channelStartValue = 0;
        while (!sliderMax)
        {
            gatherSlider.value += (channelStartValue+Time.deltaTime);

            if(gatherSlider.value >= channelDuration)
            {
                sliderMax = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
