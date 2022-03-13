namespace Abilities
{
    public class AbilityLevel
    {
        private IAbilityStats[] _stats;
        public int CurrentLevel => _level;
        public bool IsAvailableToLevelUp => _level < _stats.Length;
        private int _level = 0;
        public IAbilityStats CurrentStats => _stats[_level - 1];
        public IAbilityStats NextLevelStats => _stats[_level];

        public AbilityLevel(IAbilityStats[] stats)
        {
            _stats = stats;
        }
    
        public void LevelUp()
        {
            if(_level < _stats.Length)
                _level++;
        }
    }
}
