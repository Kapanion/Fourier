using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Color Profile", fileName = "New color profile")]
public class ColorProfile : ScriptableObject
{
    public string profileName;
    
    public Color UIBackgroundColor;
    public Color UIButtonColor;
    public Color UITextColor;
    public Color UIBoldTextColor;
    public Color UISliderColor;
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
            case ColorProfileComponent.UITextColor:
                return this.UITextColor;
            case ColorProfileComponent.UIBoldTextColor:
                return this.UIBoldTextColor;
            case ColorProfileComponent.UISliderColor:
                return this.UISliderColor;
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
    UISliderColor,
    BackgroundColor,
    VectorColor,
    VectorCircleColor,
    TrailColor,
    BrushColor,
}