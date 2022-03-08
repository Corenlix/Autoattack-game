using System;
using UnityEngine;

namespace Abilities
{
    public class Whip : MonoBehaviour
    {
        [SerializeField] private WhipProjectile _whipProjectile;
        [SerializeField] private WhipStats[] _stats;
        
        private float _remainTimeToUse;
        private int _level = 1;
        private WhipStats CurrentStats => _stats[_level - 1];
        
        public void LevelUp()
        {
            if(_level < _stats.Length)
                _level++;
        }
        
        private void Update()
        {
            _remainTimeToUse -= Time.deltaTime;
            if ((_remainTimeToUse > 0)) return;
            Use();
            _remainTimeToUse = CurrentStats.ReloadTime;
        }

        private void Use()
        {
            WhipProjectile spawnedWhip = Instantiate(_whipProjectile, transform.position, Quaternion.identity);
            spawnedWhip.transform.localScale = Vector3.Scale(spawnedWhip.transform.localScale, transform.localScale);
            spawnedWhip.Init(CurrentStats.Damage.RandomValueInRange);
        }
    }

    [Serializable]
    public class WhipStats
    {
        public float ReloadTime => _reloadTime;
        [SerializeField] private float _reloadTime;
        public IntRange Damage => _damage;
        [IntRangeSlider(0, 100)]
        [SerializeField] private IntRange _damage = new IntRange(8, 15);
    }
}
