using System;
using UnityEngine;

namespace Entities
{
    public class PlayerLevel : MonoBehaviour
    {
        public int Experience { get; private set; }

        public int ExperienceForNextLevel { get; private set; } = 25;

        public int CurrentLevel { get; private set; } = 1;

        public event Action ExperienceChanged;
        public event Action LevelChanged;

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
            Experience = 0;
            ExperienceForNextLevel *= 2;
            CurrentLevel++;
            LevelChanged?.Invoke();
        }
    }
}