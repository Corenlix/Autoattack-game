using Entities;
using UnityEngine;

namespace Abilities.Projectiles
{
    public class OrbitProjectile : MonoBehaviour
    {
        private OrbitStats _stats;
        
        public void Init(OrbitStats stats)
        {
            _stats = stats;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Enemy enemy))
            {
                enemy.TryHit(_stats.Damage.RandomValueInRange, enemy.transform.position - transform.position);
            }
        }
    }
}
