using System;
using Abilities.Description;
using Abilities.Projectiles;
using Entities;
using UnityEngine;

namespace Abilities
{
    public class Whip : Ability
    {
        [SerializeField] private Projectile<WhipStats> _projectile;
        [SerializeField] private WhipStats[] _stats;
        private ProjectilePool<WhipStats> _pool;
        private TimerAction _timer;

        private void Update()
        {
            _timer.Update();
        }

        public override void Init(Player abilityOwner)
        {
            AbilityLevel = new AbilityLevel(_stats);
        }

        protected override void OnReachFirstLevel()
        {
            _pool = new ProjectilePool<WhipStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
        }

        protected override string BuildDescription()
        {
            var currentStats = (WhipStats) AbilityLevel.CurrentStats;
            var nextLevelStats = (WhipStats) AbilityLevel.NextLevelStats;
            return new DescriptionBuilder(new DescriptionVariable[]
            {
                new IntRangeDescriptionVariable(VariableName.Damage, currentStats.Damage, nextLevelStats.Damage),
                new FloatDescriptionVariable(VariableName.ReloadTime, currentStats.ReloadTime, nextLevelStats.ReloadTime),
                new IntDescriptionVariable(VariableName.ProjectilesCount, currentStats.ProjectilesCount, nextLevelStats.ProjectilesCount),
            }).Build();
        }

        private void Use()
        {
            for (int i = 0; i < ((WhipStats) AbilityLevel.CurrentStats).ProjectilesCount; i++)
            {
                Vector3 offset = 0.25f*i * Vector3.up;
                var createdProjectile = _pool.Create((WhipStats) AbilityLevel.CurrentStats, transform.position + offset);
                var projectileScale = createdProjectile.transform.localScale;
                projectileScale.x *= Mathf.Pow(-1, i);
                createdProjectile.transform.localScale = projectileScale;
            }
        }

        private float GetReloadTime()
        {
            return ((WhipStats) AbilityLevel.CurrentStats).ReloadTime;
        }
    }

    [Serializable]
    public class WhipStats : IAbilityStats
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