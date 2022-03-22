using System;
using System.Collections;
using System.Collections.Generic;
using Abilities.Description;
using Abilities.Projectiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Abilities
{
    public class Orbit : Ability
    {
        [SerializeField] private OrbitProjectile _projectilePrefab;
        [SerializeField] private OrbitStats[] _stats;
        private OrbitProjectile _spawnedProjectile;
        private float _orbitParameter;
    
        protected override void Init()
        {
            AbilityLevel = new AbilityLevel(_stats);
        }

        protected override string BuildDescription()
        {
            var currentStats = (OrbitStats) AbilityLevel.CurrentStats;
            var nextLevelStats = (OrbitStats) AbilityLevel.NextLevelStats;
            
            return new DescriptionBuilder(new DescriptionVariable[]
            {
                new IntRangeDescriptionVariable(VariableName.Damage, currentStats.Damage, nextLevelStats.Damage),
                new FloatDescriptionVariable(VariableName.OrbitRadius, currentStats.OrbitRadius, nextLevelStats.OrbitRadius),
                new FloatDescriptionVariable(VariableName.TurnoverPeriod, currentStats.TurnoverPeriod, nextLevelStats.TurnoverPeriod),
            }).Build();
        }

        protected override void OnReachFirstLevel()
        {
            _spawnedProjectile = Instantiate(_projectilePrefab);
            UpdateProjectileStats();

            AbilityLevel.LevelChanged += UpdateProjectileStats;
        }

        private void UpdateProjectileStats()
        {
            _spawnedProjectile.Init((OrbitStats)AbilityLevel.CurrentStats);
        }

        private void Update()
        {
            float orbitPeriod = ((OrbitStats) AbilityLevel.CurrentStats).TurnoverPeriod;
            float orbitRadius = ((OrbitStats) AbilityLevel.CurrentStats).OrbitRadius;
        
            _orbitParameter += Time.deltaTime * Mathf.PI * 2 / orbitPeriod; 
            Vector3 projectilePosition = transform.position + new Vector3(orbitRadius * Mathf.Cos(_orbitParameter), orbitRadius * Mathf.Sin(_orbitParameter));
            Quaternion projectileRotation = Quaternion.Euler(0, 0, _orbitParameter * Mathf.Rad2Deg);
            _spawnedProjectile.transform.SetPositionAndRotation(projectilePosition, projectileRotation);
        }

        private void OnDestroy()
        {
            AbilityLevel.LevelChanged -= UpdateProjectileStats;
        }
    }

    [Serializable]
    public class OrbitStats : IAbilityStats
    {
        public float  OrbitRadius => _orbitRadius;
        [SerializeField] private float _orbitRadius;

        public IntRange Damage => _damage;
        [IntRangeSlider(0, 100)]
        [SerializeField] private IntRange _damage;

        public float TurnoverPeriod => turnoverPeriod;
        [SerializeField] private float turnoverPeriod;
    }
}