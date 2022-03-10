using System;
using UnityEngine;

namespace Abilities
{
    public class Whip : Ability
    {
        [SerializeField] private Projectile<WhipStats> _projectile;
        [SerializeField] private WhipStats[] _stats;
        private ProjectilePool<WhipStats> _pool;
        private float _remainTimeToUse;
        private AbilityLevel<WhipStats> _abilityLevel;
        private TimerAction _timer;
        
        public override void LevelUp()
        {
            _abilityLevel.LevelUp();
        }

        public override void Init()
        {
            _abilityLevel = new AbilityLevel<WhipStats>(_stats);
            _pool = new ProjectilePool<WhipStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
        }
        
        private void Update()
        {
            _timer.Update();
        }

        private void Use()
        {
            _pool.Create(_abilityLevel.CurrentStats);
        }

        private float GetReloadTime()
        {
            return _abilityLevel.CurrentStats.ReloadTime;
        }
    }

    [Serializable]
    public class WhipStats : IAbilityStats
    {
        public float ReloadTime => _reloadTime;
        [SerializeField] private float _reloadTime;
        public IntRange Damage => _damage;
        [IntRangeSlider(0, 100)]
        [SerializeField] private IntRange _damage = new IntRange(8, 15);
    }
}
