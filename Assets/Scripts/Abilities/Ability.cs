using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        public Sprite Icon => _icon;
        [SerializeField] private Sprite _icon;
        public string Name => _name;
        [SerializeField] private string _name;
        
        private void Start()
        {
            Init();
        }

        public abstract void LevelUp();
        public abstract string GetDescription();
        public abstract int GetLevel();
        public abstract void Init();
    }
}
