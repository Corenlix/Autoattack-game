using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Abilities
{
    public class MagicProjectile : MonoBehaviour
    {
        private Enemy _target;
        private int _minDamage;
        private int _maxDamage;
        private float _speed;
        private float _timeToDestroyWithoutEnemy = 10f;
        
        [SerializeField] private Rigidbody2D _rigidbody;

        public void Init(int minDamage, int maxDamage, float speed)
        {
            _minDamage = minDamage;
            _maxDamage = maxDamage;
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
                int damage = Random.Range(_minDamage, _maxDamage);
                if(enemy.TryDealDamage(damage, knockbackDirection))
                    Destroy(gameObject);
            }
        }
    }
}
