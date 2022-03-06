using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Experience : MonoBehaviour
{
    [SerializeField] private int _addingExperience;
    private static readonly int Take = Animator.StringToHash("Take");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Level>(out var level))
        {
            level.AddExperience(_addingExperience);
            GetComponent<Animator>().SetTrigger(Take);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
