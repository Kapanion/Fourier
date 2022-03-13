using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerSampleInputter : MonoBehaviour
{
    public Vector2[] coords;
    public int numberOfVectors;

    public FourierDrawer drawer;

    [ContextMenu("Apply")]
    void Apply()
    {
        drawer.Init(coords, numberOfVectors);
    }

    private void Start()
    {
        Apply();
    }
}
