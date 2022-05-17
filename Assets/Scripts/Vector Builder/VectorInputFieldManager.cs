using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Complex = System.Numerics.Complex;

public class VectorInputFieldManager : MonoBehaviour
{
    private List<VectorInputField> fields = new List<VectorInputField>();
    public GameObject fieldPrefab;
    public Transform fieldsParent;
    public int vectorNumberLimit = 200;

    [Space]
    public FourierDrawer fourierDrawer;
    private Action redrawStopped;

    [Space]
    public Button[] buttons;

    private bool simulationActive = false;

    private void Start()
    {
        for (int i = 0; i < fieldsParent.childCount; i++)
        {
            Destroy(fieldsParent.GetChild(i).gameObject);
        }

        redrawStopped = delegate { fourierDrawer.DisplayVectorsAtZeroTimeWithCoefficients(GetCoefficients()); };

        AddField();
    }

    public void AddField()
    {
        if (fields.Count >= vectorNumberLimit) return;
        VectorInputField newField = Instantiate(fieldPrefab).GetComponent<VectorInputField>();
        newField.transform.SetParent(fieldsParent, false);
        newField.SetFrequencyText(FourierFunction.IndexToFrequency(fields.Count));
        newField.redraw = redrawStopped;
        fields.Add(newField);
    }

    public void RemoveField()
    {
        if (fields.Count <= 1) return;
        var field = fields[fields.Count - 1];
        fields.RemoveAt(fields.Count - 1);
        Destroy(field.gameObject);
    }

    private Complex[] GetCoefficients()
    {
        List < Complex > coefs = new List<Complex>();
        foreach (var field in fields)
        {
            coefs.Add(field.GetCoefficient());
        }
        return coefs.ToArray();
    }

    private void SetInteractableAll(bool interactable)
    {
        foreach (var field in fields)
        {
            field.SetInteractable(interactable);
        }
        foreach(var button in buttons)
        {
            button.interactable = interactable;
        }
    }

    private void StartSimulation()
    {
        SetInteractableAll(false);
        fourierDrawer.Init(GetCoefficients());
    }

    private void StopSimulation()
    {
        SetInteractableAll(true);
        fourierDrawer.Reset();
        redrawStopped();
    }

    public void ToggleSimulation()
    {
        if (simulationActive) StopSimulation();
        else StartSimulation();

        simulationActive = !simulationActive;
    }
}
