using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables
{
    //Coords of plays for the cards 
    public static Vector2 playerCardHoldPosition, aiCardHoldPosition;

    public static Vector2 playerCardFlippPosition, aiCardFlippPosition;
    public static bool is_on = true;
}

static class Extension
{
    public static T PopAt<T>(this List<T> list, int index)
    {
        T r = list[index];
        list.RemoveAt(index);
        return r;
    }

    /// <summary>
    /// Converts the anchoredPosition of the first RectTransform to the second RectTransform,
    /// taking into consideration offset, anchors and pivot, and returns the new anchoredPosition
    /// </summary>
    
}
