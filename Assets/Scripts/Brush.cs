using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public int numberOfVectors;
    public FourierDrawer drawer;
    public Color color;

    private List<Vector2> vecList = new List<Vector2>();
    private TrailRenderer trlRend;

    public bool working;

    void Start()
    {
        trlRend = GetComponent<TrailRenderer>();
        Cursor.visible = true;
    }

    public void Init()
    {
        working = true;
    }

    public void Toggle()
    {
        trlRend.enabled = !trlRend.enabled;
    }

    void Update()
    {
        if (!working) return;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        trlRend.time = 1000;

        if (Input.GetMouseButton(0))
        {
            trlRend.enabled = true;
           // drawer.Hide();
            vecList.Add(pos);
            transform.position = pos;
        }
        if (Input.GetMouseButtonDown(0))
        {
            trlRend.enabled = false;
            vecList = new List<Vector2>();
            trlRend.Clear();
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            vecList = new List<Vector2>();
            trlRend.Clear();
        }
        if (Input.GetMouseButtonUp(0))
        {
            //drawer.UpdateFunction(vecList.ToArray());
            //drawer.Show();
            working = false;
            drawer.Init(vecList.ToArray(), numberOfVectors);
        }
    }
}
