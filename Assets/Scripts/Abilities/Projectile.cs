using System;
using UnityEngine;

namespace Abilities
{
    public abstract class Projectile<T> : MonoBehaviour where T : IAbilityStats
    {
        public abstract void Init(T stats);
        public event Action<Projectile<T>> Disabled;
        public event Action<Projectile<T>> Destroyed;

        private void OnDisable()
        {
            Disabled?.Invoke(this);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}