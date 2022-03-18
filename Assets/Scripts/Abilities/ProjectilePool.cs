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
        
        public Projectile<T> Create(T stats, Vector2 position)
        {
            Projectile<T> projectile = GetPoolable(position);
            projectile.Init(stats);
            return projectile;
        }
    }
}
