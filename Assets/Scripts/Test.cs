using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class Test : MonoBehaviour
{   
    public UnityEngine.Vector2[] coords;
    public int numberOfVectors;
    public bool mode;
    FourierFunction ff;

    void Start()
    {
        ff = FourierFunction.Of(coords, numberOfVectors);
        /*
        coords = new UnityEngine.Vector2[100];
        for (int i = 0; i < 25; i++)
        {
            coords[i] = new UnityEngine.Vector2(1, -1 + i * 2f / 25);
        }
        for (int i = 0; i < 25; i++)
        {
            coords[i + 25] = new UnityEngine.Vector2(1 - i * 2f / 25, 1);
        }
        for (int i = 0; i < 25; i++)
        {
            coords[i + 50] = new UnityEngine.Vector2(-1, 1 - i * 2f / 25);
        }
        for (int i = 0; i < 25; i++)
        {
            coords[i + 75] = new UnityEngine.Vector2(-1 + i * 2f / 25, -1);
        }
        */
    }

    void Update()
    {
        if (mode)
        {
            int ind = (int)((Time.time) * 25);
            ind = ind % coords.Length;
            transform.position = coords[ind] * 2;
            //print(ind);
        }
        else
        {
            float t = Time.time * 0.2f;
            t = t - (int)t;
            Complex cur = ff.ValueAt(t);
            transform.position = new UnityEngine.Vector2((float)cur.Real, (float)cur.Imaginary) * 2;
        }
    }
}
