using UnityEngine;

namespace Abilities
{
    public class WhipProjectile : MonoBehaviour, IAbility
    {
        private int _minDamage;
        private int _maxDamage;
        
        public void Init(int minDamage, int maxDamage)
        {
            _minDamage = minDamage;
            _maxDamage = maxDamage;
        }
    
        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized * Player.Knockback;
                int damage = Random.Range(_minDamage, _maxDamage);
                enemy.TryDealDamage(damage, knockbackDirection);
            }
        }
    }
}
