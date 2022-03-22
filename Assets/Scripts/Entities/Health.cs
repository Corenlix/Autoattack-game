using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxAmount;
        public float MaxAmount => _maxAmount;
        public float Amount { get; private set; }

        public void Reset()
        {
            Amount = _maxAmount;
            HealthChanged?.Invoke();
        }

        private void Start()
        {
            Reset();
        }

        public event UnityAction HealthChanged;
        public event UnityAction Died;

        public void DealDamage(float damage)
        {
            Amount -= damage;
            HealthChanged?.Invoke();
            if (Amount <= 0) Die();
        }

        private void Die()
        {
            Died?.Invoke();
        }
    }
}