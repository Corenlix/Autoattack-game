using UnityEngine;

public class ExperiencePool : GameObjectPool<ExperienceItem>
{
    public ExperiencePool(ExperienceItem experienceItemPrefab, Transform poolOwner) : base(experienceItemPrefab,
        poolOwner)
    {
    }

    public ExperienceItem Create(Vector3 position)
    {
        return GetPoolable(position);
    }
}