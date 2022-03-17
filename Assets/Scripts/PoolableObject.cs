using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public event Action<PoolableObject> Disabled;
    public event Action<PoolableObject> Destroyed;
    
    private void OnDisable()
    {
        Disabled?.Invoke(this);
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke(this);
    }
}
