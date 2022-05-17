using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererColorSetter : ColorSetter
{
    private LineRenderer myLR;

    public override void DoAwake()
    {
        myLR = GetComponent<LineRenderer>();
    }

    public override void SetColor()
    {
        myLR.startColor = GetColor();
        myLR.endColor = GetColor();
    }
}
