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
        private AbilityLevel<MagicWandStats> _abilityLevel;
        private TimerAction _timer;
        
        public override void LevelUp()
        {
            _abilityLevel.LevelUp();
        }

        public override string GetDescription()
        {
            return _abilityLevel.CurrentStats.Description;
        }

        public override int GetLevel()
        {
            return _abilityLevel.CurrentLevel;
        }

        public override void Init()
        {
            _abilityLevel = new AbilityLevel<MagicWandStats>(_stats);
            _pool = new ProjectilePool<MagicWandStats>(_projectile, transform);
            _timer = new TimerAction(GetReloadTime, Use);
        }
        
        private void Update()
        {
            _timer.Update();
        }

        private void Use()
        {
            _pool.Create(_abilityLevel.CurrentStats);
        }

        private float GetReloadTime()
        {
            return _abilityLevel.CurrentStats.ReloadTime;
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

        public string Description => _description;
        [SerializeField] private string _description;
    }
}
