using System;
using UnityEngine;

namespace Abilities
{
    public class MagicWand : MonoBehaviour
    {
        [SerializeField] private MagicProjectile _magicProjectile;
        [SerializeField] private MagicWandStats[] _stats;
        
        private float _remainTimeToUse;
        private int _level = 1;
        private MagicWandStats CurrentStats => _stats[_level - 1];

        public void LevelUp()
        {
            if(_level < _stats.Length)
                _level++;
        }
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F))
                LevelUp();
            
            _remainTimeToUse -= Time.deltaTime;
            if ((_remainTimeToUse > 0)) return;
            Use();
            _remainTimeToUse = CurrentStats.ReloadTime;
        }

        private void Use()
        {
            MagicProjectile spawnedProjectile = Instantiate(_magicProjectile, transform.position, Quaternion.identity);
            spawnedProjectile.transform.localScale = Vector3.Scale(spawnedProjectile.transform.localScale, transform.localScale);
            spawnedProjectile.Init(CurrentStats.Damage.RandomValueInRange, CurrentStats.ProjectileSpeed);
        }
    }

    [Serializable]
    public class MagicWandStats
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
