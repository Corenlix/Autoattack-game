using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        public Sprite Icon => _icon;
        [SerializeField] private Sprite _icon;
        public string Name => _name;
        [SerializeField] private string _name;

        public string Description => _description;
        [SerializeField] private string _description;

        public int Level => AbilityLevel.CurrentLevel;
        
        protected AbilityLevel AbilityLevel;
        protected DescriptionBuilder DescriptionBuilder;

        private void Start()
        {
            Init();
        }

        public void LevelUp()
        {
            _description = DescriptionBuilder.Build();
            AbilityLevel.LevelUp();
        }
        protected abstract void Init();
    }
}
