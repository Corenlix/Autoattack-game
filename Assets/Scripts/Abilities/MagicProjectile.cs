using System;
using UnityEngine;

namespace Abilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MagicProjectile : Projectile<MagicWandStats>
    {
        private Enemy _target;
        private MagicWandStats _stats;
        private float _timeToDestroyWithoutEnemy = 10f;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public override void Init(MagicWandStats stats)
        {
            _stats = stats;
            _target = Game.Instance.GetNearestEnemy(transform.position);
            _rigidbody.velocity = Vector2.right * stats.ProjectileSpeed;
        }

        private void FixedUpdate()
        {
            if (!_target)
            {
                _timeToDestroyWithoutEnemy -= Time.fixedDeltaTime;
                if(_timeToDestroyWithoutEnemy <= 0)
                    gameObject.SetActive(false);
                return;
            }

            Vector2 moveDirection = _target.transform.position - transform.position;
            _rigidbody.velocity = moveDirection.normalized * _stats.ProjectileSpeed;

            float rotZ = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized * Player.Knockback;
                if(enemy.TryDealDamage(_stats.Damage.RandomValueInRange, knockbackDirection))
                    gameObject.SetActive(false);
            }
        }
    }
}
