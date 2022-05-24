using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public Mode[] modes;
    public int defaultMode;

    private int currentMode;

    private void Start()
    {
        currentMode = defaultMode;
        foreach (var mode in modes)
        {
            mode.Disable();
        }
        modes[currentMode].Enable();
    }

    public void SetMode(int mode)
    {
        if (mode == currentMode) return;
        if (mode < 0 || mode >= modes.Length) return;

        modes[currentMode].Disable();
        currentMode = mode;
        modes[currentMode].Enable();
    }
}
