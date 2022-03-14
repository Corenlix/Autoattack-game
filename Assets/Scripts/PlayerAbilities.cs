using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using UnityEngine;

public class PlayerAbilities
{
    private List<Ability> _abilities;
    private Canvas _worldCanvas;
    private AbilitiesChooser _chooserPrefab;
    
    public PlayerAbilities(GameObject abilitiesContainer, Canvas worldCanvas, AbilitiesChooser chooserPrefab)
    {
        _abilities = abilitiesContainer.GetComponents<Ability>().ToList();
        _worldCanvas = worldCanvas;
        _chooserPrefab = chooserPrefab;
    }

    public void ChooseNewAbility()
    {
        var chooser = Object.Instantiate(_chooserPrefab, _worldCanvas.transform);
        chooser.Generate(_abilities);
        chooser.Chose += OnChoseAbility;
        Time.timeScale = 0;
    }

    private void OnChoseAbility(AbilitiesChooser chooser)
    {
        Time.timeScale = 1;
        chooser.Chose -= OnChoseAbility;
    }
}
