using UnityEngine;

namespace Abilities
{
    public class ProjectilePool<T> : GameObjectPool<Projectile<T>> where T : IAbilityStats
    {
        public ProjectilePool(Projectile<T> projectilePrefab, Transform poolOwner) : base(projectilePrefab, poolOwner)
        {
            
        }

        public Projectile<T> Create(T stats)
        {
            Projectile<T> projectile = GetPoolable();
            projectile.Init(stats);
            return projectile;
        }
    }
}
