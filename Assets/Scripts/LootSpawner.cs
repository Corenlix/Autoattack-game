using UnityEngine;
using UnityEngine.Serialization;

public class LootSpawner : MonoBehaviour
{
    [FormerlySerializedAs("_experiencePrefab")] [SerializeField]
    private ExperienceItem experienceItemPrefab;

    private ExperiencePool _experiencePool;

    public static LootSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else Instance = this;

        _experiencePool = new ExperiencePool(experienceItemPrefab, transform);
    }

    public void Spawn(Vector2 position, LootType lootType)
    {
        switch (lootType)
        {
            case LootType.Experience:
                _experiencePool.Create(position);
                break;
        }
    }
}

public enum LootType
{
    Experience
}