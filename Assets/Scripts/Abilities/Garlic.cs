using System;
using UnityEngine;

namespace Abilities
{
    public class Garlic : Ability
    {
        [SerializeField] private GarlicProjectile _projectile;
        [SerializeField] private GarlicStats[] _stats;
        private GarlicProjectile _spawnedProjectile;

        protected override void Init()
        {
            AbilityLevel = new AbilityLevel(_stats);

            DescriptionBuilder = new DescriptionBuilder(
                () => DescriptionBuilder.AppendIntRangeValueDescription(DescriptionVariableNames.Damage,
                    () => ((GarlicStats) AbilityLevel.NextLevelStats).Damage,
                    () => ((GarlicStats) AbilityLevel.CurrentStats).Damage),
                
                () => DescriptionBuilder.AppendFloatValueDescription(DescriptionVariableNames.ReloadTime,
                    () => ((GarlicStats) AbilityLevel.NextLevelStats).ReloadTime,
                    () => ((GarlicStats) AbilityLevel.CurrentStats).ReloadTime)
                );
        }

        private void OnEnable()
        {
            if (Level == 1)
                return;
            
            StartWork();
        }

        private void StartWork()
        {
            _spawnedProjectile = Instantiate(_projectile, transform.position, transform.rotation, transform);
        }

        private void Update()
        {
            _spawnedProjectile.Use((GarlicStats)AbilityLevel.CurrentStats);
        }
    }
    [Serializable]
    public class GarlicStats : IAbilityStats
    {
        public IntRange Damage => _damage;
        [IntRangeSlider(0, 100)]
        [SerializeField] private IntRange _damage = new IntRange(8, 15);

        public float ReloadTime => _reloadTime;
        [SerializeField] private float _reloadTime;
    }
}
