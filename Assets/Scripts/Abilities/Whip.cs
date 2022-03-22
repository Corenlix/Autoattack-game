using System;
using Abilities.Description;
using UnityEngine;

namespace Abilities
{
    public class Whip : Ability
    {
        [SerializeField] private Projectile<WhipStats> _projectile;
        [SerializeField] private WhipStats[] _stats;
        private ProjectilePool<WhipStats> _pool;
        private TimerAction _timer;
        
        protected override void Init()
        {
            AbilityLevel = new AbilityLevel(_stats);
            _pool = new ProjectilePool<WhipStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
        }

        protected override string BuildDescription()
        {
            WhipStats currentStats = (WhipStats) AbilityLevel.CurrentStats;
            WhipStats nextLevelStats = (WhipStats) AbilityLevel.NextLevelStats;
            return new DescriptionBuilder(new DescriptionVariable []
            {
                new IntRangeDescriptionVariable(VariableName.Damage, nextLevelStats.Damage, currentStats.Damage),
                new FloatDescriptionVariable(VariableName.ReloadTime, nextLevelStats.ReloadTime, currentStats.ReloadTime),
            }).Build();
        }

        private void Update()
        {
            _timer.Update();
        }

        private void Use()
        {
            _pool.Create((WhipStats)AbilityLevel.CurrentStats);
        }

        private float GetReloadTime()
        {
            return ((WhipStats)AbilityLevel.CurrentStats).ReloadTime;
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
