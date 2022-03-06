using UnityEngine;

namespace Abilities
{
    public class WhipProjectile : MonoBehaviour, IAbility
    {
        private int _minDamage;
        private int _maxDamage;
        private float _knockback;
        
        public void Init(int minDamage, int maxDamage, float knockback)
        {
            _minDamage = minDamage;
            _maxDamage = maxDamage;
            _knockback = knockback;
        }
    
        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized * _knockback;
                int damage = Random.Range(_minDamage, _maxDamage);
                enemy.DealDamage(damage, knockbackDirection);
            }
        }
    }
}
