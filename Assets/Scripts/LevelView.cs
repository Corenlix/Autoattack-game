using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private string _levelTextFormat = "LV {0}";
    [SerializeField] private Level _levelOwner;

    private void Awake()
    {
        UpdateLevelText();
        UpdateLevelText();
        _levelOwner.ExperienceChanged += UpdateProgressBar;
        _levelOwner.LevelChanged += UpdateLevelText;
    }

    private void UpdateProgressBar()
    {
        _progressBar.fillAmount = (float) _levelOwner.Experience / _levelOwner.ExperienceForNextLevel;
    }

    private void UpdateLevelText()
    {
        _levelText.text = string.Format(_levelTextFormat, _levelOwner.CurrentLevel);
    }
}
