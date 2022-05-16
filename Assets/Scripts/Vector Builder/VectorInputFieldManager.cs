using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complex = System.Numerics.Complex;

public class VectorInputFieldManager : MonoBehaviour
{
    private List<VectorInputField> fields = new List<VectorInputField>();
    public GameObject fieldPrefab;
    public Transform fieldsParent;
    public int vectorNumberLimit = 200;

    [Space]
    public FourierDrawer fourierDrawer;

    private void Start()
    {
        for (int i = 0; i < fieldsParent.childCount; i++)
        {
            Destroy(fieldsParent.GetChild(i).gameObject);
        }

        AddField();
    }

    public void AddField()
    {
        if (fields.Count >= vectorNumberLimit) return;
        VectorInputField newField = Instantiate(fieldPrefab).GetComponent<VectorInputField>();
        newField.transform.SetParent(fieldsParent, false);
        newField.SetFrequencyText(FourierFunction.IndexToFrequency(fields.Count));
        newField.redraw = delegate { fourierDrawer.DisplayVectorsAtZeroTimeWithCoefficients(GetCoefficients()); };
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

    public void StartSimulation()
    {
        fourierDrawer.Init(GetCoefficients());
    }
}
