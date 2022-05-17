using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorSetter : MonoBehaviour
{
    public ColorProfileComponent profileComponent;

    public void OnEnable()
    {
        ColorManagerSingleton.Instance.ChangeColors += SetColor;
    }

    private void OnDisable()
    {
        ColorManagerSingleton.Instance.ChangeColors -= SetColor;
    }

    public void Awake()
    {
        DoAwake();
        SetColor();
    }

    public Color GetColor() => ColorManagerSingleton.Instance.profile.GetColor(profileComponent);
    public abstract void SetColor();
    public abstract void DoAwake();
}
