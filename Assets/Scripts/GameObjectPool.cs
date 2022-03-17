using System.Collections.Generic;
using UnityEngine;

public abstract class GameObjectPool<T> where T : PoolableObject
{
    private Queue<T> _availablePoolables = new Queue<T>();
    private T _poolablePrefab;
    private Transform _transform;
    
    protected GameObjectPool(T poolablePrefab, Transform poolOwner)
    {
        _poolablePrefab = poolablePrefab;
        _transform = poolOwner;
    }
    
    protected T GetPoolable(Vector3 position)
    {
        T projectile;
        if (_availablePoolables.Count == 0)
            projectile = InstantiatePoolable(position);
        else
        {
            projectile = _availablePoolables.Dequeue();
            projectile.transform.position = position;
        }

        projectile.transform.localScale = _transform.lossyScale;
        projectile.gameObject.SetActive(true);
        return projectile;
    }

    protected T GetPoolable()
    {
        return GetPoolable(_transform.position);
    }
    
    private T InstantiatePoolable(Vector3 position)
    {
        var projectile = GameObject.Instantiate(_poolablePrefab, position, Quaternion.identity);
        projectile.Disabled += PoolableOnDisabled;
        projectile.Destroyed += PoolableOnDestroyed;
        return projectile;
    }

    private void PoolableOnDestroyed(PoolableObject projectile)
    {
        projectile.Disabled -= PoolableOnDisabled;
        projectile.Destroyed -= PoolableOnDestroyed;
    }

    private void PoolableOnDisabled(PoolableObject projectile)
    {
        _availablePoolables.Enqueue((T)projectile);
    }
}