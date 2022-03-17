using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Experience : PoolableObject
{
    [SerializeField] private int _addingExperience;
    private Animator _animator;
    private static readonly int Take = Animator.StringToHash("Take");
    private bool _took;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_took && other.TryGetComponent<Level>(out var level))
        {
            level.AddExperience(_addingExperience);
            _animator.SetTrigger(Take);
            _took = true;
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
        _animator.Rebind();
        _took = false;
    }
}
