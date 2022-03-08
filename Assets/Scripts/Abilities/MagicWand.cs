using System;
using UnityEngine;

namespace Abilities
{
    public class MagicWand : MonoBehaviour
    {
        [SerializeField] private MagicProjectile _magicProjectile;
        [SerializeField] private float _reloadTime;
        [IntRangeSlider(0, 50)]
        [SerializeField] private IntRange _damage = new IntRange(5,8);
        [SerializeField] private float _projectileSpeed;
        private float _remainTimeToUse;
        
        private void Update()
        {
            _remainTimeToUse -= Time.deltaTime;
            if ((_remainTimeToUse > 0)) return;
            Use();
            _remainTimeToUse = _reloadTime;
        }

        private void Use()
        {
            MagicProjectile spawnedProjectile = Instantiate(_magicProjectile, transform.position, Quaternion.identity);
            spawnedProjectile.transform.localScale = Vector3.Scale(spawnedProjectile.transform.localScale, transform.localScale);
            spawnedProjectile.Init(_damage.RandomValueInRange, _projectileSpeed);
        }
    }

    [Serializable]
    public class MagicWandStats
    {
        
    }
}
