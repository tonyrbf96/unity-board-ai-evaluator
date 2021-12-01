using System;
using UnityEngine;
using InteligenceEngine;

public class GameTypeDrowdownAttribute: PropertyAttribute
{
    public int selectedIndex;
    public Type[] collection;

    public GameTypeDrowdownAttribute()
    {
        this.collection = AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(Game));
        //Debug.Log("Total Games: " + collection.Length);
    }
}
