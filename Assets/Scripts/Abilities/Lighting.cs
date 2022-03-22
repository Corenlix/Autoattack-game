using System;
using Abilities.Description;
using UnityEngine;

namespace Abilities
{
    public class Lighting : Ability
    {
        [SerializeField] private Projectile<LightingStats> _projectile;
        [SerializeField] private LightingStats[] _stats;
        private ProjectilePool<LightingStats> _pool;
        private TimerAction _timer;

        protected override void Init()
        {
            AbilityLevel = new AbilityLevel(_stats);

            _pool = new ProjectilePool<LightingStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
        }

        protected override string BuildDescription()
        {
            LightingStats currentStats = (LightingStats) AbilityLevel.CurrentStats;
            LightingStats nextLevelStats = (LightingStats) AbilityLevel.NextLevelStats;
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
            _pool.Create((LightingStats)AbilityLevel.CurrentStats);
        }

        private float GetReloadTime()
        {
            return ((LightingStats)AbilityLevel.CurrentStats).ReloadTime;
        }
    }

    [Serializable]
    public class LightingStats : IAbilityStats
    {
        public float ReloadTime => _reloadTime;
        [SerializeField] private float _reloadTime;
        public IntRange Damage => _damage;
        [IntRangeSlider(0, 100)]
        [SerializeField] private IntRange _damage = new IntRange(8, 15);
    }
}