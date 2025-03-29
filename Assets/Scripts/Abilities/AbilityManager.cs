using System.Collections.Generic;
using UnityEngine;


public class AbilityManager : MonoBehaviour {
    [SerializeField] private List<Ability> abilities;

    public List<Ability> Abilities => abilities;

    private float attackSpeedMultiplier = 1;

    private void OnEnable()
    {
        SaveGameManager.OnSave += SaveAbilities;
        SaveGameManager.OnLoad += LoadAbilities;
    }

    private void OnDisable()
    {
        SaveGameManager.OnSave -= SaveAbilities;
        SaveGameManager.OnLoad -= LoadAbilities;
    }
    private void Update()
    {
        foreach(Ability ability in abilities){
            ability.AbilityUpdate(attackSpeedMultiplier);
            ability.Activate();
        }
    }

    public void SetAttackSpeed(float AttackSpeedMultiplier){
        this.attackSpeedMultiplier = AttackSpeedMultiplier;
    }

    private void SaveAbilities(SerializedSaveGame saveData)
    {
        saveData.abilities.Clear();
        foreach (Ability ability in abilities)
        {
            saveData.abilities.Add(new SerializedAbility
            {
                abilityID = ability.AbilityID,
                abilityLevel = ability.Level
            });
        }
    }

    private void LoadAbilities(SerializedSaveGame saveData)
    {
        foreach (SerializedAbility savedAbility in saveData.abilities)
        {
            Ability ability = abilities.Find(a => a.AbilityID == savedAbility.abilityID);
            if (ability != null)
            {
                while (ability.Level < savedAbility.abilityLevel)
                {
                    ability.UpgradeAbility();
                }
            }
        }
    }
}