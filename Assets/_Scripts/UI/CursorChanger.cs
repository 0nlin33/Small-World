using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{

    [SerializeField] private Texture2D cursorHand;
    [SerializeField] private Texture2D cursorHammer;

    private Vector2 cursorHotSpot;

    Vector2 HammerHotSpot()
    {
        cursorHotSpot = new Vector2(cursorHammer.width/2, cursorHammer.height/2);
        return cursorHotSpot;
    }

    Vector2 HandHotSpot()
    {
        cursorHotSpot = new Vector2(0, cursorHammer.height);
        return cursorHotSpot;
    }

    public void MineResource()
    {
        ChangeToHammer();
    }

    public void ClickGround()
    {
        ChangeToHand();
    }

    void ChangeToHammer()
    {
        Cursor.SetCursor(cursorHammer, HammerHotSpot(), CursorMode.Auto);
    }

    void ChangeToHand()
    {
        Cursor.SetCursor(cursorHand, HandHotSpot(), CursorMode.Auto);
    }
}
