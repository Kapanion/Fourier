using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class Tracer : MonoBehaviour
{   
    public UnityEngine.Vector2[] initialShape;
    public int numberOfVectors;
    public float frequency;

    private FourierFunction func;

    private TrailRenderer trailRend;
    private SpriteRenderer spriteRend;
    private bool hidden;

    void Start()
    {
        UpdateFunction(initialShape);
        trailRend = GetComponent<TrailRenderer>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (hidden) return;
        Complex cur = func.ValueAt(Time.time * frequency);
        transform.position = new UnityEngine.Vector2((float)cur.Real, (float)cur.Imaginary);
    }

    public void UpdateFunction(UnityEngine.Vector2[] vecArray)
    {
        func = FourierFunction.Of(vecArray, numberOfVectors);
    }

    public void Hide()
    {
        hidden = true;
        trailRend.enabled = false;
        spriteRend.enabled = false;
    }
    public void Show()
    {
        hidden = false;
        trailRend.enabled = true;
        spriteRend.enabled = true;
    }
}
