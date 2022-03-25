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
                new IntRangeDescriptionVariable(VariableName.Damage, currentStats.Damage, nextLevelStats.Damage),
                new FloatDescriptionVariable(VariableName.ReloadTime, currentStats.ReloadTime, nextLevelStats.ReloadTime),
                new IntDescriptionVariable(VariableName.ProjectilesCount, currentStats.ProjectilesCount, nextLevelStats.ProjectilesCount),
            }).Build();
        }

        protected override void OnReachFirstLevel()
        {
            _pool = new ProjectilePool<LightingStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
        }

        private void Use()
        {
            var currentStats = (LightingStats) AbilityLevel.CurrentStats;
            for (int i = 0; i < currentStats.ProjectilesCount; i++)
            {
                _pool.Create(currentStats);
            }
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

        [SerializeField] private int _projectilesCount;

        public float ReloadTime => _reloadTime;
        public IntRange Damage => _damage;
        public int ProjectilesCount => _projectilesCount;
    }
}