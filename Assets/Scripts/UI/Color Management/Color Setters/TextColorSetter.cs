using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorSetter : ColorSetter
{
    private Text myText;

    public override void DoAwake()
    {
        myText = GetComponent<Text>();
    }

    public override void SetColor()
    {
        myText.color = GetColor();
    }
}
