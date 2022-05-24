using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    public KeyCode brushCode = KeyCode.B;
    public KeyCode toggleBrushMode = KeyCode.C;

    public FourierDrawer drawer;
    public Brush brush;

    private void CleanAndEnableBrush()
    {
        drawer.Clear();
        brush.Init();
    }

    private void ToggleBrush()
    {
        brush.Toggle();
    }

    public void Reset()
    {
        // ToggleBrush();
        // CleanAndEnableBrush();
        // Calling both on the same frame causes problems for some reason.
        StartCoroutine(IReset());
    }

    IEnumerator IReset()
    {
        ToggleBrush();
        yield return null;
        CleanAndEnableBrush();
    }

    private void Update()
    {
        if (Input.GetKeyDown(brushCode)) CleanAndEnableBrush();
        if (Input.GetKeyDown(toggleBrushMode)) ToggleBrush();
    }
}
