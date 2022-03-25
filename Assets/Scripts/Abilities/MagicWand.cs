using System;
using Abilities.Description;
using Abilities.Projectiles;
using Entities;
using UnityEngine;

namespace Abilities
{
    public class MagicWand : Ability
    {
        [SerializeField] private Projectile<MagicWandStats> _projectile;
        [SerializeField] private MagicWandStats[] _stats;
        private ProjectilePool<MagicWandStats> _pool;
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
            _pool = new ProjectilePool<MagicWandStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
        }

        protected override string BuildDescription()
        {
            var currentStats = (MagicWandStats) AbilityLevel.CurrentStats;
            var nextLevelStats = (MagicWandStats) AbilityLevel.NextLevelStats;
            return new DescriptionBuilder(new DescriptionVariable[]
            {
                new IntRangeDescriptionVariable(VariableName.Damage, currentStats.Damage, nextLevelStats.Damage),
                new FloatDescriptionVariable(VariableName.ReloadTime, currentStats.ReloadTime, nextLevelStats.ReloadTime),
                new FloatDescriptionVariable(VariableName.ProjectileSpeed, currentStats.ProjectileSpeed, nextLevelStats.ProjectileSpeed)
            }).Build();
        }

        private void Use()
        {
            _pool.Create((MagicWandStats) AbilityLevel.CurrentStats);
        }

        private float GetReloadTime()
        {
            return ((MagicWandStats) AbilityLevel.CurrentStats).ReloadTime;
        }
    }

    [Serializable]
    public class MagicWandStats : IAbilityStats
    {
        [SerializeField] private float _reloadTime;

        [IntRangeSlider(0, 50)] [SerializeField]
        private IntRange _damage = new IntRange(5, 8);

        [SerializeField] private float _projectileSpeed;
        
        public float ReloadTime => _reloadTime;
        public IntRange Damage => _damage;
        public float ProjectileSpeed => _projectileSpeed;
    }
}