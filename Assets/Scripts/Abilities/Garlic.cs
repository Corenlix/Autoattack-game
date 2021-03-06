using System;
using Abilities.Description;
using Abilities.Projectiles;
using Entities;
using UnityEngine;

namespace Abilities
{
    public class Garlic : Ability
    {
        [SerializeField] private GarlicProjectile _projectilePrefab;
        [SerializeField] private GarlicStats[] _stats;
        private GarlicProjectile _spawnedProjectile;

        private void Update()
        {
            _spawnedProjectile.Use((GarlicStats) AbilityLevel.CurrentStats);
        }

        public override void Init(Player abilityOwner)
        {
            AbilityLevel = new AbilityLevel(_stats);
        }

        protected override string BuildDescription()
        {
            var currentStats = (GarlicStats) AbilityLevel.CurrentStats;
            var nextLevelStats = (GarlicStats) AbilityLevel.NextLevelStats;
            return new DescriptionBuilder(new DescriptionVariable[]
            {
                new IntRangeDescriptionVariable(VariableName.Damage, currentStats.Damage, nextLevelStats.Damage),
                new FloatDescriptionVariable(VariableName.ReloadTime, currentStats.ReloadTime, nextLevelStats.ReloadTime)
            }).Build();
        }

        protected override void OnReachFirstLevel()
        {
            _spawnedProjectile = Instantiate(_projectilePrefab, transform.position, transform.rotation, transform);
        }
    }

    [Serializable]
    public class GarlicStats : IAbilityStats
    {
        [IntRangeSlider(0, 100)] [SerializeField]
        private IntRange _damage = new IntRange(8, 15);

        [SerializeField] private float _reloadTime;
        public IntRange Damage => _damage;

        public float ReloadTime => _reloadTime;
    }
}