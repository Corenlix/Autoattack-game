using UnityEngine;

namespace Abilities
{
    public class Whip : MonoBehaviour
    {
        [SerializeField] private WhipProjectile _whipProjectile;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _remainTimeToUse;
        [SerializeField] private int _minDamage;
        [SerializeField] private int _maxDamage;
        
        private void Update()
        {
            _remainTimeToUse -= Time.deltaTime;
            if ((_remainTimeToUse > 0)) return;
            Use();
            _remainTimeToUse = _reloadTime;
        }

        private void Use()
        {
            WhipProjectile spawnedWhip = Instantiate(_whipProjectile, transform.position, Quaternion.identity);
            spawnedWhip.transform.localScale = Vector3.Scale(spawnedWhip.transform.localScale, transform.localScale);
            spawnedWhip.Init(_minDamage, _maxDamage);
        }
    }
}
