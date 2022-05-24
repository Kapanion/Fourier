using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardcodedRenderer : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.O;
    public GameObject optionsUI;
    public HardcodedItem chosen;

    public FourierDrawer drawer;
    public int numberOfVectors = 200;

    void ToggleOptions()
    {
        optionsUI.SetActive(!optionsUI.activeSelf);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey)) ToggleOptions();
    }

    [ContextMenu("Apply Current")]
    public void ApplyCurrent()
    {
        LogicManager.Instance.brush.Disable();

        var vectors = HardcodedBS.options[chosen].Clone() as Vector2[];

        const float desiredMaxWidth = 8;
        const float desiredMaxHeight = 8;
        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY= float.MinValue;

        foreach (var vec in vectors)
        {
            minX = Math.Min(minX, vec.x);
            minY = Math.Min(minY, vec.y);
            maxX = Math.Max(maxX, vec.x);
            maxY = Math.Max(maxY, vec.y);
        }

        float width = maxX - minX;
        float height = maxY - minY;
        float scaleBy = width > height ? desiredMaxWidth / width : desiredMaxHeight / height;
        // float scaleBy = 0.02f;

        for (int i = 0; i < vectors.Length; i++) {
            vectors[i].x -= (minX + maxX) / 2;
            vectors[i].x *= scaleBy;
            vectors[i].y -= (minY + maxY) / 2;
            vectors[i].y *= scaleBy;
            vectors[i].y *= -1.0f;
        }

        drawer.Init(vectors, numberOfVectors);
    }

    public void Choose(int item)
    {
        chosen = (HardcodedItem)item;
    }
    
    public void ChooseAndApply(int item)
    {
        chosen = (HardcodedItem)item;
        ApplyCurrent();
    }
}
