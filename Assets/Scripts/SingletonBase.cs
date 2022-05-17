using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    public static T Instance { get; private set; }

    protected void Awake()
    {
        // Destroy this object if we already have a Singleton defined
        if (Instance != null)
        {
            if ( Instance == (T)this ) return;
            Destroy(gameObject);
            return;
        }
        Instance = (T)this;
        DoAwake();
    }

    protected virtual void DoAwake() { }
}
