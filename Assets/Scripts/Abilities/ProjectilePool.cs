using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class ProjectilePool<T> where T : IAbilityStats
    {
        private Queue<Projectile<T>> _availableProjectiles = new Queue<Projectile<T>>();
        private Projectile<T> _projectilePrefab;
        private Transform _transform;

        public ProjectilePool(Projectile<T> projectilePrefab, Transform poolOwner)
        {
            _projectilePrefab = projectilePrefab;
            _transform = poolOwner;
        }

        public Projectile<T> Create(T stats)
        {
            Projectile<T> projectile;
            if (_availableProjectiles.Count == 0)
                projectile = InstantiateProjectile();
            else
            {
                projectile = _availableProjectiles.Dequeue();
                projectile.transform.position = _transform.position;
            }

            projectile.transform.localScale = _transform.lossyScale;
            projectile.gameObject.SetActive(true);
            projectile.Init(stats);
            return projectile;
        }
        
        private Projectile<T> InstantiateProjectile()
         {
             var projectile = GameObject.Instantiate(_projectilePrefab, _transform.position, Quaternion.identity);
             projectile.Disabled += ProjectileOnDisabled;
             projectile.Destroyed += ProjectileOnDestroyed;
             return projectile;
         }

        private void ProjectileOnDestroyed(Projectile<T> projectile)
        {
            projectile.Disabled -= ProjectileOnDisabled;
            projectile.Destroyed -= ProjectileOnDestroyed;
        }

        private void ProjectileOnDisabled(Projectile<T> projectile)
        {
            _availableProjectiles.Enqueue(projectile);
        }
    }
}
