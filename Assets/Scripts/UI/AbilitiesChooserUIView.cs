using System;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI
{
    public class AbilitiesChooserUIView : MonoBehaviour
    {
        private const int ChooseAbilitiesCount = 3;
        [SerializeField] private AbilityUIView _abilityUIViewPrefab;
        private List<AbilityUIView> _abilityViews;

        private void OnDestroy()
        {
            Unsubscribe();
        }

        public event Action<AbilitiesChooserUIView> Chose;

        public void Generate(List<Ability> availableAbilities)
        {
            Unsubscribe();
            _abilityViews = new List<AbilityUIView>();
            availableAbilities = availableAbilities.ToList();
            var createdAbilityViews = 0;
            while (createdAbilityViews < ChooseAbilitiesCount && availableAbilities.Count > 0)
            {
                var currentAbilityIndex = Random.Range(0, availableAbilities.Count());
                var currentAbility = availableAbilities[currentAbilityIndex];
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

            foreach (var abilityView in _abilityViews) abilityView.Clicked -= OnChose;
        }
    }
}