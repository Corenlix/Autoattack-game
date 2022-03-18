using UnityEngine;

namespace Abilities
{
    public class LightingProjectile : Projectile<LightingStats>
    {
        private LightingStats _stats;
        private Animator _animator;
        private Enemy _target;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void Init(LightingStats stats)
        {
            _stats = stats;
            _target = Game.Instance.RandomEnemy;
            if (!_target)
                return;
            transform.position = _target.transform.position;
        }

        private void Disable()
        {
            gameObject.SetActive(false);
            _animator.Rebind();
        }

        private void DealDamage()
        {
            if (!_target)
                return;
            _target.TryHit(_stats.Damage.RandomValueInRange, Vector2.zero);
        }
    }
}