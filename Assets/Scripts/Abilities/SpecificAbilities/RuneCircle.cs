using System.Collections.Generic;
using DG.Tweening;
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
            if(enemy) enemy.TakeDamage(new DamageArgs { Damage = this.Damage * _damageModifier });
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
    public override void UpgradeAbility()
    {
        base.UpgradeAbility();
        if(_level %2 == 0){
            Vector3 endValue = this.transform.localScale * 1.15f;
            endValue.y = 1;
            transform.DOScale(endValue, 0.3f);//Increase scale by 15% per second level
        }
        else{
            _damageModifier+= 0.1f;//Increase damage by 10% every second level
        }
    }
}