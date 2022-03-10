namespace Abilities
{
    public class AbilityLevel<T> where T : IAbilityStats
    {
        private T[] _stats;
        public int CurrentLevel => _level;
        private int _level = 1;
        public T CurrentStats => _stats[_level - 1];
    
        public AbilityLevel(T[] stats)
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
