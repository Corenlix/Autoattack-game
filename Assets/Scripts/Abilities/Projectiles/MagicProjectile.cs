using Entities;
using UnityEngine;

namespace Abilities.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MagicProjectile : Projectile<MagicWandStats>
    {
        private Rigidbody2D _rigidbody;
        private MagicWandStats _stats;
        private Enemy _target;
        private float _timeToDestroyWithoutEnemy = 10f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!_target)
            {
                _timeToDestroyWithoutEnemy -= Time.fixedDeltaTime;
                if (_timeToDestroyWithoutEnemy <= 0)
                    gameObject.SetActive(false);
                return;
            }

            Vector2 moveDirection = _target.transform.position - transform.position;
            _rigidbody.velocity = moveDirection.normalized * _stats.ProjectileSpeed;

            var rotZ = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                Vector2 knockbackDirection = enemy.transform.position - transform.position;
                if (enemy.TryHit(_stats.Damage.RandomValueInRange, knockbackDirection))
                    gameObject.SetActive(false);
            }
        }

        public override void Init(MagicWandStats stats)
        {
            _stats = stats;
            _target = Game.Instance.NearestEnemy(transform.position);
            _rigidbody.velocity = Vector2.right * stats.ProjectileSpeed;
        }
    }
}