using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RadioButton : MonoBehaviour
{
    public Image buttonImage;
    public UnityEvent onSelect;
    public UnityEvent onDeselect;

    public void Select()
    {
        onSelect.Invoke();
    }

    public void Deselect()
    {
        onDeselect.Invoke();
    }
}
