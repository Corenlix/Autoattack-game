using System;
using UnityEngine;

namespace Abilities
{
    [RequireComponent(typeof(Animator))]
    [Serializable]
    public class WhipProjectile : Projectile<WhipStats>
    {
        private WhipStats _stats;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void Init(WhipStats stats)
        {
            _stats = stats;
        }
    
        private void Disable()
        {
            gameObject.SetActive(false);
            _animator.Rebind();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                Vector2 knockbackDirection = enemy.transform.position - transform.position;
                enemy.TryHit(_stats.Damage.RandomValueInRange, knockbackDirection);
            }
        }
    }
}
