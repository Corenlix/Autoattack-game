using System;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private Experience _experiencePrefab;
    
    public static LootSpawner Instance => _instance;
    private static LootSpawner _instance;

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else _instance = this;
    }

    public void Spawn(Vector2 position, LootType lootType)
    {
        switch (lootType)
        {
            case(LootType.Experience):
                Instantiate(_experiencePrefab, position, Quaternion.identity);
                break;
        }
    }
}

public enum LootType
{
    Experience,
}
