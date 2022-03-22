using System;

namespace Abilities
{
    public class AbilityLevel
    {
        public event Action LevelChanged;
        
        private readonly IAbilityStats[] _stats;

        public AbilityLevel(IAbilityStats[] stats)
        {
            _stats = stats;
        }

        public int CurrentLevel { get; private set; }

        public bool IsAvailableToLevelUp => CurrentLevel < _stats.Length;
        public IAbilityStats CurrentStats => _stats[CurrentLevel - 1];
        public IAbilityStats NextLevelStats => _stats[CurrentLevel];

        public void LevelUp()
        {
            if (CurrentLevel >= _stats.Length)
                return;
            
            CurrentLevel++;
            LevelChanged?.Invoke();
        }
    }
}