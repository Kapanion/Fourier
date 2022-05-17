using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroundColorSetter : ColorSetter
{
    private Camera mySR;

    public override void DoAwake()
    {
        mySR = GetComponent<Camera>();
    }

    public override void SetColor()
    {
        mySR.backgroundColor = GetColor();
    }
}
