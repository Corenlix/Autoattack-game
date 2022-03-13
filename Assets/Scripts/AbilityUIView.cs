using System;
using Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIView : MonoBehaviour
{
    public event Action Clicked;
    
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    private Ability _ability;

    public void Init(Ability ability)
    {
        _ability = ability;
        _name.text = ability.Name;
        _description.text = ability.Description;
        _level.text = ability.Level.ToString();
        _icon.sprite = ability.Icon;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _ability.LevelUp();
        Clicked?.Invoke();
    }
}
