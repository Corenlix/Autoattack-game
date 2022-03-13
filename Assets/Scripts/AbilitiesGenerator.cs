using System.Collections.Generic;
using System.Linq;
using Abilities;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbilitiesGenerator : MonoBehaviour
{
    [SerializeField] private AbilityUIView _abilityUIViewPrefab;
    private List<AbilityUIView> _abilityViews;
    private const int AbilitiesCount = 3;
    
    public void Generate()
    {
        Unsubscribe();
        _abilityViews = new List<AbilityUIView>();
        var availableAbilities = Game.Instance.CurrentPlayer.GetComponentsInChildren<Ability>().ToList();
        int createdAbilityViews = 0;
        while(createdAbilityViews < AbilitiesCount && availableAbilities.Count > 0)
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
        abilityView.Clicked += OnSelected;
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
