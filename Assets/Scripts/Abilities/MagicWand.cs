using System;
using UnityEngine;

namespace Abilities
{
    public class MagicWand : Ability
    {
        [SerializeField] private Projectile<MagicWandStats> _projectile;
        [SerializeField] private MagicWandStats[] _stats;
        private ProjectilePool<MagicWandStats> _pool;
        private float _remainTimeToUse;
        private TimerAction _timer;
        
        
        protected override void Init()
        {
            AbilityLevel = new AbilityLevel(_stats);
            DescriptionBuilder = new DescriptionBuilder(
                    () => DescriptionBuilder.AppendFloatValueDescription(DescriptionVariableNames.ReloadTime,
                        () => ((MagicWandStats) AbilityLevel.NextLevelStats).ReloadTime,
                        () => ((MagicWandStats) AbilityLevel.CurrentStats).ReloadTime),
                    
                    () => DescriptionBuilder.AppendIntRangeValueDescription(DescriptionVariableNames.Damage,
                        () => ((MagicWandStats) AbilityLevel.NextLevelStats).Damage,
                        () => ((MagicWandStats) AbilityLevel.CurrentStats).Damage),
                    
                    () => DescriptionBuilder.AppendFloatValueDescription(DescriptionVariableNames.ProjectileSpeed,
                        () => ((MagicWandStats) AbilityLevel.NextLevelStats).ProjectileSpeed,
                        () => ((MagicWandStats) AbilityLevel.CurrentStats).ProjectileSpeed)
                );
            _pool = new ProjectilePool<MagicWandStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
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
