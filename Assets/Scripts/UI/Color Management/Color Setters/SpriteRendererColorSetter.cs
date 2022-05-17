using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererColorSetter : ColorSetter
{
    private SpriteRenderer mySR;

    public override void DoAwake()
    {
        mySR = GetComponent<SpriteRenderer>();
    }

    public override void SetColor()
    {
        mySR.color = GetColor();
    }
}
