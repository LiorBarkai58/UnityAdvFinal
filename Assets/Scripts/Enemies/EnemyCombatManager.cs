using UnityEngine;


public class EnemyCombatManager : CombatManager {
    [SerializeField] private CombatData combatData;
    private void OnEnable(){
        currentMaxHealth = combatData.MaxHealth;
        currentHealth = combatData.MaxHealth;
    }
}