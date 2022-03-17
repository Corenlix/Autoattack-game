using UnityEngine;

public class ExperiencePool : GameObjectPool<Experience>
{
    public ExperiencePool(Experience experiencePrefab, Transform poolOwner) : base(experiencePrefab, poolOwner)
    {
    }

    public Experience Create(Vector3 position)
    {
        return GetPoolable(position);
    }
}
