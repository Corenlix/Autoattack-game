using System;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbilitiesChooser : MonoBehaviour
{
    [SerializeField] private AbilityUIView _abilityUIViewPrefab;
    public event Action<AbilitiesChooser> Chose;
    private List<AbilityUIView> _abilityViews;
    private const int ChooseAbilitiesCount = 3;
    
    public void Generate(List<Ability> availableAbilities)
    {
        Unsubscribe();
        _abilityViews = new List<AbilityUIView>();
        availableAbilities = availableAbilities.ToList();
        int createdAbilityViews = 0;
        while(createdAbilityViews < ChooseAbilitiesCount && availableAbilities.Count > 0)
        {
            int currentAbilityIndex = Random.Range(0, availableAbilities.Count());
            Ability currentAbility = availableAbilities[currentAbilityIndex];
            availableAbilities.RemoveAt(currentAbilityIndex);
            if (!currentAbility.IsAvailableToLevelUp)
                continue;
            
            SpawnAbilityView(currentAbility);
            createdAbilityViews++;
        }
    }

    private void SpawnAbilityView(Ability ability)
    {
        var abilityView = Instantiate(_abilityUIViewPrefab, transform);
        _abilityViews.Add(abilityView);
        abilityView.Init(ability);
        abilityView.Clicked += OnChose;
    }

    private void OnChose()
    {
        Chose?.Invoke(this);
        Destroy(gameObject);
    }

    private void Unsubscribe()
    {
        if (_abilityViews == null)
            return;

        foreach (var abilityView in _abilityViews)
        {
            abilityView.Clicked -= OnChose;
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

}
