using System;
using Abilities.Description;
using Abilities.Projectiles;
using Entities;
using UnityEngine;

namespace Abilities
{
    public class Knife : Ability
    {
        [SerializeField] private KnifeProjectile _projectile;
        [SerializeField] private KnifeStats[] _stats;
        private ProjectilePool<KnifeStats> _pool;
        private TimerAction _timer;
        private Player _abilityOwner;

        private void Update()
        {
            _timer.Update();
        }

        public override void Init(Player abilityOwner)
        {
            AbilityLevel = new AbilityLevel(_stats);
            _abilityOwner = abilityOwner;
        }

        protected override void OnReachFirstLevel()
        {
            _pool = new ProjectilePool<KnifeStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
        }

        protected override string BuildDescription()
        {
            var currentStats = (KnifeStats) AbilityLevel.CurrentStats;
            var nextLevelStats = (KnifeStats) AbilityLevel.NextLevelStats;
            return new DescriptionBuilder(new DescriptionVariable[]
            {
                new IntRangeDescriptionVariable(VariableName.Damage, nextLevelStats.Damage, currentStats.Damage),
                new FloatDescriptionVariable(VariableName.ReloadTime, nextLevelStats.ReloadTime,
                    currentStats.ReloadTime)
            }).Build();
        }

        private void Use()
        {
            var knife = (KnifeProjectile)_pool.Create((KnifeStats) AbilityLevel.CurrentStats);
            knife.SetMoveDirection(_abilityOwner.MoveDirection);
        }

        private float GetReloadTime()
        {
            return ((KnifeStats) AbilityLevel.CurrentStats).ReloadTime;
        }
    }
    
    [Serializable]
    public class KnifeStats : IAbilityStats
    {
        [SerializeField] private float _reloadTime;

        [IntRangeSlider(0, 100)] [SerializeField]
        private IntRange _damage = new IntRange(8, 15);

        [SerializeField] private float _projectileSpeed;

        public float ReloadTime => _reloadTime;
        public IntRange Damage => _damage;
        public float ProjectileSpeed => _projectileSpeed;
    }
}
