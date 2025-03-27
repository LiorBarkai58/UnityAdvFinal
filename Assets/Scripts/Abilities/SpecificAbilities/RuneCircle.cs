using System.Collections.Generic;
using UnityEngine;

public class RuneCircle : Ability
{

    private List<CombatManager> enemiesInRange = new List<CombatManager>();

    [SerializeField] private float Damage;

    public override bool AbilityLogic()
    {
        if(enemiesInRange.Count == 0) return false;

        // Create a copy of the list to iterate over, different abilities can kill an enemy during the iteration and change the original list
        List<CombatManager> enemiesSnapshot = new List<CombatManager>(enemiesInRange);

        foreach (CombatManager enemy in enemiesSnapshot)
        {
            if(enemy) enemy.TakeDamage(new DamageArgs { Damage = this.Damage });
        }
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")){
            CombatManager enemyCombat = other.GetComponent<CombatManager>();
            if(enemyCombat){
                enemiesInRange.Add(enemyCombat);
                enemyCombat.OnDeath += HandleEnemyDeath;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy")){
            CombatManager enemyCombat = other.GetComponent<CombatManager>();
            if(enemyCombat && enemiesInRange.Contains(enemyCombat)){
                enemiesInRange.Remove(enemyCombat);
                enemyCombat.OnDeath -= HandleEnemyDeath;

            }
        }
    }

    private void HandleEnemyDeath(CombatManager enemy){
        if(enemiesInRange.Contains(enemy)){
            enemiesInRange.Remove(enemy);
        }
    }
}