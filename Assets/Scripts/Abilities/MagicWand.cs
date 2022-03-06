using UnityEngine;

namespace Abilities
{
    public class MagicWand : MonoBehaviour
    {
        [SerializeField] private MagicProjectile _magicProjectile;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _remainTimeToUse;
        [SerializeField] private int _minDamage;
        [SerializeField] private int _maxDamage;
        [SerializeField] private float _projectileSpeed;
        
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
            spawnedProjectile.Init(_minDamage, _maxDamage, _projectileSpeed);
        }
    }
}
