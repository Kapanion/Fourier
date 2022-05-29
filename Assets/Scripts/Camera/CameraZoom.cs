using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : SingletonBase<CameraZoom>
{
    new public Camera camera;
    public float speed;
    private float scrollY = 0;

    public float minZoom = 0.1f;
    public float maxZoom = 100;

    protected override void DoAwake()
    {
        camera ??= Camera.main;
    }

    private void Update()
    {
        scrollY -= Input.mouseScrollDelta.y;
    }

    private void FixedUpdate()
    {
        if (scrollY == 0) return;

        float zoomAmount = speed * scrollY;

        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize + zoomAmount, minZoom, maxZoom);

        scrollY = 0;
    }
}
