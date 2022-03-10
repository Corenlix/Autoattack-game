using Abilities;

public class AbilityLevel<T> where T : IAbilityStats
{
    private T[] _stats;
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
