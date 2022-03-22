using UnityEngine;

namespace Abilities.Projectiles
{
    public class ProjectilePool<T> : GameObjectPool<Projectile<T>> where T : IAbilityStats
    {
        public ProjectilePool(Projectile<T> projectilePrefab, Transform poolOwner) : base(projectilePrefab, poolOwner)
        {
        }

        public Projectile<T> Create(T stats)
        {
            var projectile = GetPoolable();
            projectile.Init(stats);
            return projectile;
        }

        public Projectile<T> Create(T stats, Vector2 position)
        {
            var projectile = GetPoolable(position);
            projectile.Init(stats);
            return projectile;
        }
    }
}