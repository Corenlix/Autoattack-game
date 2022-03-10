using System;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;

        private void Start()
        {
            Init();
        }

        public abstract void LevelUp();
        public abstract void Init();
    }
}
