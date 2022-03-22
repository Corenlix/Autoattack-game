using System;
using Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AbilityUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;
        private Ability _ability;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public event Action Clicked;

        public void Init(Ability ability)
        {
            _ability = ability;
            _name.text = ability.Name;
            _description.text = ability.NextLevelDescription;
            _level.text = (ability.Level + 1).ToString();
            _icon.sprite = ability.Icon;
        }

        private void OnClick()
        {
            _ability.LevelUp();
            Clicked?.Invoke();
        }
    }
}