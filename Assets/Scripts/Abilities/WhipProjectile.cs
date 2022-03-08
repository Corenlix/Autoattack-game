using UnityEngine;

namespace Abilities
{
    public class WhipProjectile : MonoBehaviour, IAbility
    {
        private int _damage;
        
        public void Init(int damage)
        {
            _damage = damage;
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
                enemy.TryDealDamage(_damage, knockbackDirection);
            }
        }
    }
}
