using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    //This class is responsible in changing the cursor when hovering over different objects according what has been defined

    // variables and refrences
    [SerializeField] private Texture2D cursorHand;
    [SerializeField] private Texture2D cursorHammer;

    private Vector2 cursorHotSpot; //Cursor hotspot is the point from which the clickin occurs

    //change the hotspot ofthe cursor to middle of the cursor image
    Vector2 HammerHotSpot()
    {
        cursorHotSpot = new Vector2(cursorHammer.width/2, cursorHammer.height/2);
        return cursorHotSpot;
    }

    //change the hotspot ofthe cursor to top left of the cursor image
    Vector2 HandHotSpot()
    {
        cursorHotSpot = new Vector2(0, cursorHammer.height);
        return cursorHotSpot;
    }

    //expose the function changing cursor to hammer for other classes to refrence
    public void MineResource()
    {
        ChangeToHammer();
    }

    //expose the function changing cursor to hand for other classes to refrence
    public void ClickGround()
    {
        ChangeToHand();
    }

    //change the cursor to hammer
    void ChangeToHammer()
    {
        Cursor.SetCursor(cursorHammer, HammerHotSpot(), CursorMode.Auto);
    }

    //change the cursor to hand
    void ChangeToHand()
    {
        Cursor.SetCursor(cursorHand, HandHotSpot(), CursorMode.Auto);
    }
}
