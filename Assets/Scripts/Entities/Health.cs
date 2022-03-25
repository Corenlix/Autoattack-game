using System;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour
    {
        public event Action HealthChanged;
        public event Action Died;
        
        [SerializeField] private float _maxAmount;
        public float MaxAmount => _maxAmount;
        public float Amount { get; private set; }
        
        public void DealDamage(float damage)
        {
            Amount -= damage;
            HealthChanged?.Invoke();
            if (Amount <= 0) Die();
        }

        public void Reset()
        {
            Amount = _maxAmount;
            HealthChanged?.Invoke();
        }

        private void Start()
        {
            Reset();
        }
        
        private void Die()
        {
            Died?.Invoke();
        }
    }
}