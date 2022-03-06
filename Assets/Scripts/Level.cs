using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    public event Action ExperienceChanged;
    public event Action LevelChanged;

    public int Experience => _experience;
    private int _experience;

    public int ExperienceForNextLevel => _experienceForNextLevel;
    private int _experienceForNextLevel = 25;

    public int CurrentLevel => _currentLevel;
    private int _currentLevel = 1;
    
    public void AddExperience(int amount)
    {
        int experienceNotEnoughToLevelUp = _experienceForNextLevel - _experience;
        while (amount >= experienceNotEnoughToLevelUp)
        {
            amount -= experienceNotEnoughToLevelUp;
            LevelUp();
        }

        _experience += amount;
        ExperienceChanged?.Invoke();
    }

    private void LevelUp()
    {
        _experience = 0;
        _experienceForNextLevel *= 2;
        _currentLevel++;
        LevelChanged?.Invoke();
    }
}
