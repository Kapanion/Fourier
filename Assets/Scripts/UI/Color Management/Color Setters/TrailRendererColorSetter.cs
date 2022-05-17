using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererColorSetter : ColorSetter
{
    private TrailRenderer myTR;

    public override void DoAwake()
    {
        myTR = GetComponent<TrailRenderer>();
    }

    public override void SetColor()
    {
        myTR.startColor = GetColor();
        myTR.endColor = GetColor();
    }
}
