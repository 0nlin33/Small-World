using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Unnecessary class for now, but people might choose to use it to have a more dynamic storage for ther data
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
