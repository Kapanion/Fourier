using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RadioButtonManager : MonoBehaviour
{
    private const int DESELECTED = -1;
    
    public RadioButton[] buttons;
    public int defaultSelection;

    [Space] public UnityEvent onAnySelected;

    private int selected = DESELECTED;

    private void Start()
    {
        Select(defaultSelection);
    }
    
    private bool InvalidIndex(int index) => (index < 0 || index >= buttons.Length);

    public void Select(int index)
    {
        if (index == selected) return;
        
        if (InvalidIndex(index))
        {
            DeselectAll();
            return;
        }

        if (selected == DESELECTED)
        {
            onAnySelected.Invoke();
        }
        else
        {
            buttons[selected].Deselect();
        }
        
        selected = index;
        buttons[selected].Select();
    }

    public void DeselectAll()
    {
        if (selected == DESELECTED) return;
        buttons[selected].Deselect();
        selected = DESELECTED;
    }
}
