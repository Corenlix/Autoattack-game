using UnityEngine;
using UnityEngine.Serialization;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        public Sprite Icon => _icon;
        [SerializeField] private Sprite _icon;
        public string Name => _name;
        [SerializeField] private string _name;

        public string NextLevelNextLevelDescription => _nextLevelDescription;
        private string _nextLevelDescription;

        public string Description => _description;
        [SerializeField] private string _description;
        
        public int Level => AbilityLevel.CurrentLevel;

        public bool IsAvailableToLevelUp => AbilityLevel.IsAvailableToLevelUp;
        
        protected AbilityLevel AbilityLevel;
        protected DescriptionBuilder DescriptionBuilder;

        private void Awake()
        {
            _nextLevelDescription = _description;
            enabled = false;
            Init();
        }

        public void LevelUp()
        {
            if (!AbilityLevel.IsAvailableToLevelUp)
                return;

            if (AbilityLevel.CurrentLevel == 0)
                enabled = true;

            AbilityLevel.LevelUp();
            _description = _nextLevelDescription;
            if(AbilityLevel.IsAvailableToLevelUp)
                _nextLevelDescription = DescriptionBuilder.Build();
        }
        protected abstract void Init();
    }
}
