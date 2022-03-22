using System.Collections.Generic;
using System.Linq;
using Abilities;
using UI;
using UnityEngine;

namespace Entities
{
    public class PlayerAbilities
    {
        private readonly List<Ability> _abilities;
        private readonly AbilitiesChooserUIView _chooserUIViewPrefab;
        private readonly Canvas _worldCanvas;

        public PlayerAbilities(GameObject abilitiesContainer, Canvas worldCanvas,
            AbilitiesChooserUIView chooserUIViewPrefab)
        {
            _abilities = abilitiesContainer.GetComponents<Ability>().ToList();
            _worldCanvas = worldCanvas;
            _chooserUIViewPrefab = chooserUIViewPrefab;
        }

        public void ChooseNewAbility()
        {
            var chooser = Object.Instantiate(_chooserUIViewPrefab, _worldCanvas.transform);
            chooser.Generate(_abilities);
            chooser.Chose += OnChoseAbility;
            Time.timeScale = 0;
        }

        private void OnChoseAbility(AbilitiesChooserUIView chooserUIView)
        {
            Time.timeScale = 1;
            chooserUIView.Chose -= OnChoseAbility;
        }
    }
}