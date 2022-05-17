using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorSetter : ColorSetter
{
    private Image myImage;

    public override void DoAwake()
    {
        myImage = GetComponent<Image>();
    }

    public override void SetColor()
    {
        myImage.color = GetColor();
    }
}
