using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private Experience _experiencePrefab;
    
    public static LootSpawner Instance => _instance;
    private static LootSpawner _instance;

    private ExperiencePool _experiencePool;

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else _instance = this;

        _experiencePool = new ExperiencePool(_experiencePrefab, transform);
    }

    public void Spawn(Vector2 position, LootType lootType)
    {
        switch (lootType)
        {
            case(LootType.Experience):
                _experiencePool.Create(position);
                break;
        }
    }
}

public enum LootType
{
    Experience,
}
