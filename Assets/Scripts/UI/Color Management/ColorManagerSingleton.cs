using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManagerSingleton : SingletonBase<ColorManagerSingleton>
{
    public event Action ChangeColors = null;
    public ColorProfile[] profiles;

    public ColorProfile profile;
    private int currProfile;

    private void Start()
    {
        RefreshColors();
    }

    private void RefreshColors()
    {
        profile = profiles[currProfile];
        if (ChangeColors == null)
        {
            Debug.Log("No subscribers to the ChangeColors event");
            return;
        }
        ChangeColors();
    }

    [ContextMenu("Refresh Colors (Editor Only)")]
    public void RefreshColorsEditor()
    {
        if (!Application.isEditor) return;
        Awake();
        foreach (var obj in FindObjectsOfType<ColorSetter>())
        {
            obj.Awake();
            obj.OnEnable();
        }
        RefreshColors();
    }

    public void SetProfile(int index)
    {
        currProfile = index;
        RefreshColors();        
    }
}
