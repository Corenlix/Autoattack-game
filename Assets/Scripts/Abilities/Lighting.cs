using System;
using Abilities.Description;
using Abilities.Projectiles;
using Entities;
using UnityEngine;

namespace Abilities
{
    public class Lighting : Ability
    {
        [SerializeField] private Projectile<LightingStats> _projectile;
        [SerializeField] private LightingStats[] _stats;
        private ProjectilePool<LightingStats> _pool;
        private TimerAction _timer;

        private void Update()
        {
            _timer.Update();
        }

        public override void Init(Player abilityOwner)
        {
            AbilityLevel = new AbilityLevel(_stats);
        }

        protected override string BuildDescription()
        {
            var currentStats = (LightingStats) AbilityLevel.CurrentStats;
            var nextLevelStats = (LightingStats) AbilityLevel.NextLevelStats;
            return new DescriptionBuilder(new DescriptionVariable[]
            {
                new IntRangeDescriptionVariable(VariableName.Damage, nextLevelStats.Damage, currentStats.Damage),
                new FloatDescriptionVariable(VariableName.ReloadTime, nextLevelStats.ReloadTime,
                    currentStats.ReloadTime)
            }).Build();
        }

        protected override void OnReachFirstLevel()
        {
            _pool = new ProjectilePool<LightingStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
        }

        private void Use()
        {
            _pool.Create((LightingStats) AbilityLevel.CurrentStats);
        }

        private float GetReloadTime()
        {
            return ((LightingStats) AbilityLevel.CurrentStats).ReloadTime;
        }
    }

    [Serializable]
    public class LightingStats : IAbilityStats
    {
        [SerializeField] private float _reloadTime;

        [IntRangeSlider(0, 100)] [SerializeField]
        private IntRange _damage = new IntRange(8, 15);

        public float ReloadTime => _reloadTime;
        public IntRange Damage => _damage;
    }
}