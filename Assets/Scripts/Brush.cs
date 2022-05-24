using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public int numberOfVectors;
    public FourierDrawer drawer;

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

    public void Clear()
    {
        working = false;
    }

    public void Toggle()
    {
        trlRend.enabled = !trlRend.enabled;
        if (!trlRend.enabled) Clear();
    }

    public void Disable()
    {
        trlRend.enabled = false;
        Clear();
    }
    
    public void Enable()
    {
        trlRend.enabled = true;
        Init();
    }

    void Update()
    {
        if (!working) return;

        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     working = false;
        //     return;
        // }

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
