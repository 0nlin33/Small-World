using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources
{
    private int woodAmount;
    private int metalAmount;

    public int WoodAmount
    {
        get{ return woodAmount;}
    }

    public void NewWoodAmount(int newWood)
    {
        if( woodAmount != newWood )
        {
            woodAmount = newWood;
        }
    }

    public int MetalAmount
    {
        get { return metalAmount;}
    }

    public void NewMetalAmount(int newMetal)
    {
        if ((metalAmount != newMetal))
        {
            metalAmount = newMetal;
        }
    }
}
