using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mode : MonoBehaviour
{
    public GameObject[] associatedUIElements;
    public UnityEvent onEnable;
    public UnityEvent onDisable;

    private void SetActivity(bool active)
    {
        foreach (var obj in associatedUIElements)
        {
            obj.SetActive(active);
        }
    }

    public void Enable()
    {
        onEnable.Invoke();
        SetActivity(true);
    }
    
    public void Disable()
    {
        onDisable.Invoke();
        SetActivity(false);
    }
}
