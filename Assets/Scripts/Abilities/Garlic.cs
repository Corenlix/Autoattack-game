using System;
using Abilities.Description;
using UnityEngine;

namespace Abilities
{
    public class Garlic : Ability
    {
        [SerializeField] private GarlicProjectile _projectilePrefab;
        [SerializeField] private GarlicStats[] _stats;
        private GarlicProjectile _spawnedProjectile;

        protected override void Init()
        {
            AbilityLevel = new AbilityLevel(_stats);
        }

        protected override string BuildDescription()
        {
            GarlicStats currentStats = (GarlicStats) AbilityLevel.CurrentStats;
            GarlicStats nextLevelStats = (GarlicStats) AbilityLevel.NextLevelStats;
            return new DescriptionBuilder(new DescriptionVariable []
            {
                new IntRangeDescriptionVariable(VariableName.Damage, nextLevelStats.Damage, currentStats.Damage),
                new FloatDescriptionVariable(VariableName.ReloadTime, nextLevelStats.ReloadTime, currentStats.ReloadTime),
            }).Build();
        }

        private void OnEnable()
        {
            if (Level == 1)
                return;
            
            StartWork();
        }

        private void StartWork()
        {
            _spawnedProjectile = Instantiate(_projectilePrefab, transform.position, transform.rotation, transform);
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
