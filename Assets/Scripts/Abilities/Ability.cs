using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private string _description;

        protected AbilityLevel AbilityLevel;
        public Sprite Icon => _icon;
        public string Name => _name;

        public string NextLevelDescription { get; private set; }

        public string Description => _description;

        public int Level => AbilityLevel.CurrentLevel;

        public bool IsAvailableToLevelUp => AbilityLevel.IsAvailableToLevelUp;

        private void Awake()
        {
            NextLevelDescription = _description;
            enabled = false;
            Init();
        }

        public void LevelUp()
        {
            if (!AbilityLevel.IsAvailableToLevelUp)
                return;

            AbilityLevel.LevelUp();
            if (AbilityLevel.CurrentLevel == 1)
            {
                enabled = true;
                OnReachFirstLevel();
            }
            
            _description = NextLevelDescription;
            if (AbilityLevel.IsAvailableToLevelUp)
                NextLevelDescription = BuildDescription();
        }

        protected abstract void Init();

        protected abstract string BuildDescription();

        protected abstract void OnReachFirstLevel();
    }
}