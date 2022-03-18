using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class Lighting : Ability
    {
        [SerializeField] private Projectile<LightingStats> _projectile;
        [SerializeField] private LightingStats[] _stats;
        private ProjectilePool<LightingStats> _pool;
        private float _remainTimeToUse;
        private TimerAction _timer;

        protected override void Init()
        {
            AbilityLevel = new AbilityLevel(_stats);

            DescriptionBuilder = new DescriptionBuilder(
                () => DescriptionBuilder.AppendFloatValueDescription(DescriptionVariableNames.ReloadTime,
                    () => ((LightingStats) AbilityLevel.NextLevelStats).ReloadTime,
                    () => ((LightingStats) AbilityLevel.CurrentStats).ReloadTime),

                () => DescriptionBuilder.AppendIntRangeValueDescription(DescriptionVariableNames.Damage,
                    () => ((LightingStats) AbilityLevel.NextLevelStats).Damage,
                    () => ((LightingStats) AbilityLevel.CurrentStats).Damage));
            
            _pool = new ProjectilePool<LightingStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
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