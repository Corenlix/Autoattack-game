using System;
using Abilities.Description;
using UnityEngine;

namespace Abilities
{
    public class MagicWand : Ability
    {
        [SerializeField] private Projectile<MagicWandStats> _projectile;
        [SerializeField] private MagicWandStats[] _stats;
        private ProjectilePool<MagicWandStats> _pool;
        private TimerAction _timer;

        protected override void Init()
        {
            AbilityLevel = new AbilityLevel(_stats);
            _pool = new ProjectilePool<MagicWandStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
        }
        
        protected override string BuildDescription()
        {
            MagicWandStats currentStats = (MagicWandStats) AbilityLevel.CurrentStats;
            MagicWandStats nextLevelStats = (MagicWandStats) AbilityLevel.NextLevelStats;
            return new DescriptionBuilder(new DescriptionVariable []
            {
                new IntRangeDescriptionVariable(VariableName.Damage, nextLevelStats.Damage, currentStats.Damage),
                new FloatDescriptionVariable(VariableName.ReloadTime, nextLevelStats.ReloadTime, currentStats.ReloadTime),
                new FloatDescriptionVariable(VariableName.ProjectileSpeed, nextLevelStats.ProjectileSpeed, currentStats.ProjectileSpeed),
            }).Build();
        }
        
        private void Update()
        {
            _timer.Update();
        }

        private void Use()
        {
            _pool.Create((MagicWandStats)AbilityLevel.CurrentStats);
        }

        private float GetReloadTime()
        {
            return ((MagicWandStats)AbilityLevel.CurrentStats).ReloadTime;
        }
    }

    [Serializable]
    public class MagicWandStats : IAbilityStats
    {
        public float ReloadTime => _reloadTime;
        [SerializeField] private float _reloadTime;
        public IntRange Damage => _damage;
        [IntRangeSlider(0, 50)]
        [SerializeField] private IntRange _damage = new IntRange(5,8);

        public float ProjectileSpeed => _projectileSpeed;
        [SerializeField] private float _projectileSpeed;
    }
}
