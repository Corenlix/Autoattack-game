using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Abilities
{
    public class MagicProjectile : MonoBehaviour
    {
        private Enemy _target;
        private int _damage;
        private float _speed;
        private float _timeToDestroyWithoutEnemy = 10f;
        
        [SerializeField] private Rigidbody2D _rigidbody;

        public void Init(int damage, float speed)
        {
            _damage = damage;
            _speed = speed;
            
            _target = Game.Instance.GetNearestEnemy(transform.position);
            _rigidbody.velocity = Vector2.right * speed;
        }

        private void FixedUpdate()
        {
            if (!_target)
            {
                _timeToDestroyWithoutEnemy -= Time.fixedDeltaTime;
                if(_timeToDestroyWithoutEnemy <= 0)
                    Destroy(gameObject);
                return;
            }

            Vector2 moveDirection = _target.transform.position - transform.position;
            _rigidbody.velocity = moveDirection.normalized * _speed;

            float rotZ = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized * Player.Knockback;
                if(enemy.TryDealDamage(_damage, knockbackDirection))
                    Destroy(gameObject);
            }
        }
    }
}
