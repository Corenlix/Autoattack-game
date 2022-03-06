using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float MaxAmount => _maxAmount;
    [SerializeField] private float _maxAmount;
    public float Amount => _amount;
    [SerializeField] private float _amount;
    
    public event UnityAction HealthChanged;
    public event UnityAction Died;
    
    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        _amount = _maxAmount;
        HealthChanged?.Invoke();
    }
        
    public void DealDamage(float damage)
    {
        _amount -= damage;
        HealthChanged?.Invoke();
        if (_amount <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Died?.Invoke();
    }
}
