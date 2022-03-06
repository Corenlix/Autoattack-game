using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _healthStripe;
    [SerializeField] private Health _health;

    private void Fill()
    {
        _healthStripe.fillAmount = _health.Amount / _health.MaxAmount;
    }
    
    private void OnEnable()
    {
        Fill();
        _health.HealthChanged += Fill;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= Fill;
    }

    private void LateUpdate()
    {
        if(transform.lossyScale.x < 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
