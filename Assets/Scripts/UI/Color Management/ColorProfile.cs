using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Color Profile", fileName = "New color profile")]
public class ColorProfile : ScriptableObject
{
    public string profileName;
    
    public Color UIBackgroundColor;
    public Color UIButtonColor;
    public Color UIButtonColor2;
    public Color UITextColor;
    public Color UITextColor2;
    public Color UIButtonTextColor;
    public Color UIBoldTextColor;
    [FormerlySerializedAs("UISliderColor")] public Color UISliderBackgroundColor;
    public Color BackgroundColor;
    public Color VectorColor;
    public Color VectorCircleColor;
    public Color TrailColor;
    public Color BrushColor;
    public Color Other;

    public Color GetColor(ColorProfileComponent component)
    {
        switch (component)
        {
            case ColorProfileComponent.UIBackgroundColor:
                return this.UIBackgroundColor;
            case ColorProfileComponent.UIButtonColor:
                return this.UIButtonColor;
            case ColorProfileComponent.UIButtonColor2:
                return this.UIButtonColor2;
            case ColorProfileComponent.UITextColor:
                return this.UITextColor;
            case ColorProfileComponent.UITextColor2:
                return this.UITextColor2;
            case ColorProfileComponent.UIButtonTextColor:
                return this.UIButtonTextColor;
            case ColorProfileComponent.UIBoldTextColor:
                return this.UIBoldTextColor;
            case ColorProfileComponent.UISliderBackgroundColor:
                return this.UISliderBackgroundColor;
            case ColorProfileComponent.BackgroundColor:
                return this.BackgroundColor;
            case ColorProfileComponent.VectorColor:
                return this.VectorColor;
            case ColorProfileComponent.VectorCircleColor:
                return this.VectorCircleColor;
            case ColorProfileComponent.TrailColor:
                return this.TrailColor;
            case ColorProfileComponent.BrushColor:
                return this.BrushColor;
            default:
                return this.Other;
        }
    }
}

public enum ColorProfileComponent
{
    UIBackgroundColor,
    UIButtonColor,
    UITextColor,
    UIBoldTextColor,
    UISliderBackgroundColor,
    BackgroundColor,
    VectorColor,
    VectorCircleColor,
    TrailColor,
    BrushColor,
    UITextColor2,
    UIButtonColor2,
    UIButtonTextColor,
}