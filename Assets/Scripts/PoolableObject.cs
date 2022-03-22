using System;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    private void OnDisable()
    {
        Disabled?.Invoke(this);
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke(this);
    }

    public event Action<PoolableObject> Disabled;
    public event Action<PoolableObject> Destroyed;
}