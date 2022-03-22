using Entities;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private string _levelTextFormat = "LV {0}";

        [FormerlySerializedAs("_levelOwner")] [SerializeField]
        private PlayerLevel playerLevelOwner;

        private void Awake()
        {
            UpdatePlayerLevelText();
            UpdatePlayerLevelText();
            playerLevelOwner.ExperienceChanged += UpdateProgressBar;
            playerLevelOwner.LevelChanged += UpdatePlayerLevelText;
        }

        private void UpdateProgressBar()
        {
            _progressBar.fillAmount = (float) playerLevelOwner.Experience / playerLevelOwner.ExperienceForNextLevel;
        }

        private void UpdatePlayerLevelText()
        {
            _levelText.text = string.Format(_levelTextFormat, playerLevelOwner.CurrentLevel);
        }
    }
}