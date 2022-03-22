using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Abilities.Projectiles
{
    public class GarlicProjectile : MonoBehaviour
    {
        private readonly List<Enemy> _damagedEnemy = new List<Enemy>();
        private readonly List<Enemy> _overlapEnemies = new List<Enemy>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy)) _overlapEnemies.Add(enemy);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy)) _overlapEnemies.Remove(enemy);
        }

        public void Use(GarlicStats stats)
        {
            foreach (var overlapEnemy in _overlapEnemies)
            {
                if (!overlapEnemy)
                {
                    _overlapEnemies.Remove(overlapEnemy);
                    continue;
                }

                if (_damagedEnemy.Contains(overlapEnemy))
                    continue;

                float damage = stats.Damage.RandomValueInRange;
                overlapEnemy.TryHit(damage, overlapEnemy.transform.position - transform.position);
                _damagedEnemy.Add(overlapEnemy);
                StartCoroutine(ReloadForEnemy(overlapEnemy, stats.ReloadTime));
            }
        }

        private IEnumerator ReloadForEnemy(Enemy enemy, float time)
        {
            yield return new WaitForSeconds(time);
            _damagedEnemy.Remove(enemy);
        }
    }
}