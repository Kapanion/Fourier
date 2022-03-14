using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    public KeyCode brushCode = KeyCode.B;
    public KeyCode toggleBrushMode = KeyCode.C;

    public FourierDrawer drawer;
    public Brush brush;

    void CleanAndEnableBrush()
    {
        drawer.Clear();
        brush.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(brushCode)) CleanAndEnableBrush();
        if (Input.GetKeyDown(toggleBrushMode)) brush.Toggle();
    }
}
