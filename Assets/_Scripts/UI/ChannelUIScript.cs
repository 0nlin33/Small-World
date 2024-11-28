using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChannelUIScript : MonoBehaviour
{
    public GameObject sliderHolder;
    public Slider gatherSlider;
    float channelDuration = 2f;

    [Header("Player Refrence")]
    [SerializeField] PlayerCollisionHandler playerHandler;


    // Start is called before the first frame update
    void Start()
    {
        sliderHolder.SetActive(false);
        playerHandler.OnStartChanneling += StartChanneling;
        playerHandler.OnResourceEnter += ChannelStatus;
        playerHandler.OnResourceDepleted += ChannelStatusChange;
    }

    bool resourceDelpted = false;

    private void ChannelStatusChange(bool obj)
    {
        resourceDelpted = obj;
    }

    private void OnDisable()
    {
        playerHandler.OnStartChanneling -= StartChanneling;
        playerHandler.OnResourceEnter -= ChannelStatus;

    }

    private void ChannelStatus(bool showChannel)
    {
        sliderHolder.SetActive(showChannel);
    }

    private void StartChanneling(bool channelStatus)
    {
        StartCoroutine(ChannelSlider());
    }

    IEnumerator ChannelSlider()
    {
        float elapsedTime = 0f;

        gatherSlider.value = 0;

        while (elapsedTime < channelDuration)
        {
            gatherSlider.value = Mathf.Lerp(0, 1, elapsedTime / channelDuration);

            elapsedTime += Time.deltaTime;

            yield return null; 
        }

        gatherSlider.value = 1;

        yield return new WaitForSeconds(.2f);
        gatherSlider.value = 0;
    }

    /*void Update()
    {
        float randValue = Random.Range(0.0f, 2.0f);
        gatherSlider.value = randValue;
    }*/
}
