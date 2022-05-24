using System.Collections;
using System.Collections.Generic;
using Complex = System.Numerics.Complex;
using UnityEngine;

public class FourierDrawer : MonoBehaviour
{
    public bool displayVectors;

    public enum TrailMode { None, Trail, Wave }
    public TrailMode trailMode;

    public Transform trailPoint;
    
    public LineRenderer waveLine;

    private FourierFunction function;
    private float[] magnitudes;

    public GameObject vectorPrefab;
    public Transform vectorParent;
    private Transform[] vectors;

    private bool started = false;

    public float fourierTimeScale = 0.2f;
    public float waveLineSpeedMultiplier = 1;

    float waveLineLastTime = 0;

    float flashPoint = 0; // last time game was reset, needed to not mess up drawing logic

    [ContextMenu("Clear")]
    public void Clear()
    {
        started = false;
        function = null;
        vectors = null;
        magnitudes = null;
        trailPoint.GetComponent<TrailRenderer>().Clear();
        waveLine.positionCount = 0;
        if (Application.isEditor) DestroyImmediate(vectorParent.gameObject);
        else Destroy(vectorParent.gameObject);
        vectorParent = new GameObject("Vector Parent").transform;
    }

    public void Init(Vector2[] vals, int numberOfVectors)
    {
        if (started) Reset();

        function = FourierFunction.Of(vals, numberOfVectors);

        SetupVectors();

        started = true;
    }

    public void Init(Complex[] coefficients)
    {
        //if (started)
        Reset();

        function = new FourierFunction(coefficients);

        SetupVectors();

        started = true;
    }

    public void Reset()
    {
        flashPoint = Time.time;
        Clear();
    }

    void SetupVectors()
    {
        var data = GetVectorPositionsAndAngles(0);

        SetupMagnitudes();
        SpawnVectors(data);
        DisplayVectors(data);
    }

    void SetupMagnitudes()
    {
        magnitudes = new float[function.Size];

        if (displayVectors)
            for (int i = 0; i < function.Size; i++)
            {
                int freq = FourierFunction.IndexToFrequency(i);

                magnitudes[i] = function.GetMagnitude(freq);
            }
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

        float time = (Time.time - flashPoint) * fourierTimeScale;

        if (trailMode == TrailMode.Trail) DisplayTrail(time);
        else if (trailMode == TrailMode.Wave) DisplayTrailWave(time);

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

    public void DisplayVectorsAtZeroTimeWithCoefficients(Complex[] coefficients)
    {
        Clear();
        function = new FourierFunction(coefficients);
        SetupVectors();
        DisplayVectors(GetVectorPositionsAndAngles(0));
    }

    void DisplayTrail(float time)
    {
        Complex result = function.ValueAt(time);
        Vector3 position = new Vector3((float)result.Real, (float)result.Imaginary, 0);

        trailPoint.position = position;
    }

    void DisplayTrailWave(float time)
    {
        Complex result = function.ValueAt(time);
        Vector3 position = new Vector3((float)result.Real, (float)result.Imaginary, 0);

        int last = waveLine.positionCount++;
        var position1 = waveLine.transform.position;
        waveLine.SetPosition(last, position - position1);

        position1 += Vector3.right * ((time - waveLineLastTime) * waveLineSpeedMultiplier);
        waveLine.transform.position = position1;

        waveLineLastTime = time;
    }

    public void SetTrailMode(TrailMode mode)
    {
        if (mode == trailMode) return;

        trailPoint.GetComponent<TrailRenderer>().Clear();
        waveLine.positionCount = 0;
        trailMode = mode;
    }

    public void EnableVectors()
    {
        if (displayVectors) return;

        displayVectors = true;

        if (vectorParent.childCount == 0) SetupVectors();
        else vectorParent.gameObject.SetActive(true);
    }

    public void DisableVectors()
    {
        if (!displayVectors) return;

        displayVectors = false;
        vectorParent.gameObject.SetActive(false);
    }
}
