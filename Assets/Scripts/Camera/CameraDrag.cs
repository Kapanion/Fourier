using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : SingletonBase<CameraDrag>
{
    public float speed;
    new public Camera camera;

    // public bool Dragging => FourierDrawer.Instance.started;

    private Vector2 drag;
    private Vector3 mouseLastPos;

    private bool startedDragging = false;

    protected override void DoAwake()
    {
        camera ??= Camera.main;
    }

    private void Update()
    {
        if (!FourierDrawer.Instance.started) return;

        if (!startedDragging && !Helpers.IsPointerOverUIObject() && Input.GetMouseButtonDown(0))
        {
            startedDragging = true;
            mouseLastPos = Input.mousePosition;
        }

        if (!startedDragging) return;

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseDelta = mouseLastPos - Input.mousePosition;

            drag += (Vector2)mouseDelta;

            mouseLastPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) startedDragging = false;
    }

    private void FixedUpdate()
    {
        if (drag == Vector2.zero) return;

        camera.transform.position += new Vector3(drag.x, drag.y) * speed;

        drag = Vector2.zero;
    }
}
