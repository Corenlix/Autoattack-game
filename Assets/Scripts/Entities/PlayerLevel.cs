using System;
using UnityEngine;

namespace Entities
{
    public class PlayerLevel : MonoBehaviour
    {
        public int Experience { get; private set; }
        public int CurrentLevel { get; private set; } = 1;
        
        public int ExperienceForNextLevel => _experiencesForLevel[CurrentLevel - 1];
        public event Action ExperienceChanged;
        public event Action LevelChanged;

        [SerializeField] private int[] _experiencesForLevel;
        
        public void AddExperience(int amount)
        {
            var experienceNotEnoughToLevelUp = ExperienceForNextLevel - Experience;
            while (amount >= experienceNotEnoughToLevelUp)
            {
                amount -= experienceNotEnoughToLevelUp;
                LevelUp();
            }

            Experience += amount;
            ExperienceChanged?.Invoke();
        }

        private void LevelUp()
        {
            if (CurrentLevel >= _experiencesForLevel.Length)
                throw new Exception();
            
            Experience = 0;
            CurrentLevel++;
            LevelChanged?.Invoke();
        }
    }
}