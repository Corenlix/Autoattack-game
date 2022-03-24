using Entities;
using UnityEngine;

namespace Abilities.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class KnifeProjectile : Projectile<KnifeStats>
    {
        private KnifeStats _stats;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                Vector2 knockbackDirection = enemy.transform.position - transform.position;
                if(enemy.TryHit(_stats.Damage.RandomValueInRange, knockbackDirection))
                    gameObject.SetActive(false);
            }
        }

        public void SetMoveDirection(Vector2 direction)
        {
            if (direction == Vector2.zero)
                direction = transform.localScale.x > 0 ? transform.right : -transform.right;
            _rigidbody.velocity = direction * _stats.ProjectileSpeed;
            
            var rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);
        }

        public override void Init(KnifeStats stats)
        {
            _stats = stats;
        }
    }
}
