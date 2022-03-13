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
        private TimerAction _timer;
        

        protected override void Init()
        {
            AbilityLevel = new AbilityLevel(_stats);

            DescriptionBuilder = new DescriptionBuilder(
                () => DescriptionBuilder.AppendFloatValueDescription(DescriptionVariableNames.ReloadTime,
                    () => ((WhipStats) AbilityLevel.NextLevelStats).ReloadTime,
                    () => ((WhipStats) AbilityLevel.CurrentStats).ReloadTime),

                () => DescriptionBuilder.AppendIntRangeValueDescription(DescriptionVariableNames.Damage,
                    () => ((WhipStats) AbilityLevel.NextLevelStats).Damage,
                    () => ((WhipStats) AbilityLevel.CurrentStats).Damage));
            
            _pool = new ProjectilePool<WhipStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
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
