using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Complex = System.Numerics.Complex;

public class VectorInputField : MonoBehaviour
{
    public InputField lengthField;
    public InputField rotationField;
    public Text frequency;

    private float length;
    private float rotation;
    public Action redraw;

    void Start()
    {
        lengthField.onEndEdit.AddListener(UpdateLength);
        rotationField.onEndEdit.AddListener(UpdateRotation);
    }

    private void UpdateLength(string s)
    {
        if (s == "") return;
        length = float.Parse(s);
        redraw();
    }

    private void UpdateRotation(string s)
    {
        if (s == "") return;
        rotation = float.Parse(s);
        redraw();
    }

    public void SetFrequencyText(int freq)
    {
        frequency.text = freq.ToString();
    }

    public void SetInteractable(bool interactable)
    {
        lengthField.interactable = interactable;
        rotationField.interactable = interactable;
    }

    public Complex GetCoefficient()
    {
        var v = Quaternion.Euler(0, 0, rotation) * Vector3.right * length;
        return new Complex(v.x, v.y);
    }
}
