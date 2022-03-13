using System.Collections;
using System.Collections.Generic;
using Complex = System.Numerics.Complex;
using UnityEngine;

public class FourierDrawer : MonoBehaviour
{
    public bool displayVectors;
    public bool displayResultTrail;

    public Transform trailPoint;

    private FourierFunction function;
    private float[] magnitudes;

    public GameObject vectorPrefab;
    public Transform vectorParent;
    private Transform[] vectors;

    private bool started = false;

    public float fourierTimeScale = 0.2f;

    [ContextMenu("Clear")]
    public void Clear()
    {
        started = false;
        function = null;
    }

    public void Init(Vector2[] vals, int numberOfVectors)
    {
        if (started) return;

        function = FourierFunction.Of(vals, numberOfVectors);

        magnitudes = new float[function.Size];

        if (displayVectors) for(int i = 0; i < function.Size; i++)
        {
            int freq = FourierFunction.IndexToFrequency(i);

            magnitudes[i] = function.GetMagnitude(freq);
        }

        SetupVectors();

        started = true;
    }

    void SetupVectors()
    {
        var data = GetVectorPositionsAndAngles(0);

        SpawnVectors(data);
        DisplayVectors(data);
    }

    void SpawnVectors((Vector3, float)[] data)
    {
        vectors = new Transform[data.Length];

        for(int i = 0; i < data.Length; i++)
        {
            Vector3 pos = data[i].Item1;
            Quaternion rot = Quaternion.Euler(0, 0, data[i].Item2);

            var vector = Instantiate(vectorPrefab, pos, rot, vectorParent);
            vector.transform.localScale = Vector3.one * magnitudes[i];
            vectors[i] = vector.transform;
        }
    }

    private void FixedUpdate()
    {
        if (!started) return;

        float time = Time.time * fourierTimeScale;

        if (displayResultTrail) DisplayTrail(time);
        if (displayVectors) DisplayVectors(GetVectorPositionsAndAngles(time));
    }

    void DirectArrow(int index, Vector3 position, float angle)
    {
        vectors[index].position = position;
        vectors[index].rotation = Quaternion.Euler(0, 0, angle);
    }

    (Vector3, float)[] GetVectorPositionsAndAngles(float time)
    {
        Complex[] results = function.ValuesAt(time);
        var answer = new (Vector3, float)[results.Length];

        for (int i = 0; i < results.Length; i++)
        {
            Complex result = results[i];
            Vector3 position = new Vector3((float)result.Real, (float)result.Imaginary, 0);
            Complex next = results[i] - (i == 0 ? Complex.Zero : results[i-1]);
            float angle = Mathf.Atan2((float)next.Imaginary, (float)next.Real) * Mathf.Rad2Deg;

            answer[i] = (position, angle);
        }

        return answer;
    }

    void DisplayVectors((Vector3, float)[] data)
    {
        for(int i = 0; i < data.Length; i++) DirectArrow(i, data[i].Item1, data[i].Item2);
    }

    void DisplayTrail(float time)
    {
        Complex result = function.ValueAt(time);
        Vector3 position = new Vector3((float)result.Real, (float)result.Imaginary, 0);

        trailPoint.position = position;
    }
}
