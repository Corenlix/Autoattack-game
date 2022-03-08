using UnityEngine;

namespace Abilities
{
    public class Whip : MonoBehaviour
    {
        [SerializeField] private WhipProjectile _whipProjectile;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _remainTimeToUse;
        [IntRangeSlider(0, 100)]
        [SerializeField] private IntRange _damage = new IntRange(8, 15);
        
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
            spawnedWhip.Init(_damage.RandomValueInRange);
        }
    }
}
