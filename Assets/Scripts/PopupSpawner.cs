using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpawner : MonoBehaviour
{
    [SerializeField] private DamagePopupView _popupPrefab;
    
    public static PopupSpawner Instance => _instance;
    private static PopupSpawner _instance;

    public void SpawnPopup(Vector3 position, float damage)
    {
        DamagePopupView spawnedPopup = Instantiate(_popupPrefab, position, Quaternion.identity);
        spawnedPopup.Init(damage);
    }
    
    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
