using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public static class Helpers
{
    public static float RoundN(this float x, int n)
    {
        return (float)Math.Round(x, n);
    }

    public static float Round1(this float x)
    {
        return x.RoundN(1);
    }

    public static float Round2(this float x)
    {
        return x.RoundN(2);
    }
    
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
