using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChannelUIScript : MonoBehaviour
{
    //This script is used only to control the channeling slider which shows the progression of harvesting
    //If stopped harvesting in the middle of channeling, the resource willnot be harvested

    //Refrencing variables for data and objects
    public GameObject sliderHolder;
    public Slider gatherSlider;
    float channelDuration = 2f;

    //refrencing required script
    [Header("Player Refrence")]
    [SerializeField] PlayerCollisionHandler playerHandler;


    // Start is called before the first frame update
    void Start()
    {
        //subscribing to events from another class
        sliderHolder.SetActive(false);
        playerHandler.OnStartChanneling += StartChanneling;
        playerHandler.OnResourceEnter += ChannelStatus;
        playerHandler.OnResourceDepleted += ChannelStatusChange;
    }

    bool resourceDelpted = false;
    //corresponding function/method gets called when the event subscribed to in the class happens or is invoked
    private void ChannelStatusChange(bool obj)
    {
        resourceDelpted = obj;
    }

    //subscribing and unsubscribing is necessary to ensure that when the event happens or gets invoked, the accosicated method doesnt get called multiple times
    private void OnDisable()
    {
        playerHandler.OnStartChanneling -= StartChanneling;
        playerHandler.OnResourceEnter -= ChannelStatus;
        playerHandler.OnResourceDepleted -= ChannelStatusChange;
    }

    //associated function - associated to event
    private void ChannelStatus(bool showChannel)
    {
        sliderHolder.SetActive(showChannel);
    }

    //associated function - associated to event
    private void StartChanneling(bool channelStatus)
    {
        StartCoroutine(ChannelSlider());
    }

    //this coroutine is used to make the slider go from 0 to 1 in the same speed harvesting is happening
    IEnumerator ChannelSlider()
    {
        float elapsedTime = 0f;

        gatherSlider.value = 0; //starting vlaue of slider


        //while condition to go loop till condition is satisfied
        while (elapsedTime < channelDuration)
        {
            gatherSlider.value = Mathf.Lerp(0, 1, elapsedTime / channelDuration);

            elapsedTime += Time.deltaTime;

            yield return null; 
        }

        gatherSlider.value = 1; //ending vlaue of slider

        yield return new WaitForSeconds(.2f);
        gatherSlider.value = 0; //Reset value of slider
    }
}
