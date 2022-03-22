using Entities;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ExperienceItem : PoolableObject
{
    private static readonly int Take = Animator.StringToHash("Take");
    [SerializeField] private int _addExperience;
    private Animator _animator;
    private bool _took;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_took && other.TryGetComponent<PlayerLevel>(out var level))
        {
            level.AddExperience(_addExperience);
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