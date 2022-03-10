using System;
using System.Collections.Generic;
using Abilities;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbilitiesGenerator : MonoBehaviour
{
    [SerializeField] private AbilityUIView _abilityUIViewPrefab;
    private List<AbilityUIView> _abilityViews;
    
    public void Generate()
    {
        Unsubscribe();
        _abilityViews = new List<AbilityUIView>();
        var availableAbilities = Game.Instance.CurrentPlayer.GetComponentsInChildren<Ability>();
        for (int i = 0; i < 3; i++)
        {
            var abilityView = Instantiate(_abilityUIViewPrefab, transform);
            _abilityViews.Add(abilityView);
            int currentAbilityIndex = Random.Range(0, availableAbilities.Length);
            Ability currentAbility = availableAbilities[currentAbilityIndex];
            abilityView.Init(currentAbility);
            abilityView.Clicked += OnSelected;
        }
    }

    private void OnSelected()
    {
        Destroy(gameObject);
    }

    private void Unsubscribe()
    {
        if (_abilityViews == null)
            return;

        foreach (var abilityView in _abilityViews)
        {
            abilityView.Clicked -= OnSelected;
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

}
