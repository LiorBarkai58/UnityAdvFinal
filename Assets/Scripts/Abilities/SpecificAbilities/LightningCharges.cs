using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightningCharges : Ability
{
    [SerializeField] private DamageZone lightningEffectPrefab;

    private int _zoneCount = 1;


    public override bool AbilityLogic()
    {
        if(enemiesInRange.Count > 0){
            StartCoroutine(CreateLightningFields());
            return true;
        }
        else return false;
    }

    private IEnumerator CreateLightningFields(){
        for(int i = 0; i < _zoneCount; i++){
            if(enemiesInRange.Count > 0){//Here to make sure the enemies that were in range don't die during the cast
                CombatManager enemyTarget = enemiesInRange[Random.Range(0, enemiesInRange.Count-1)];
                DamageZone currentProjectile = Instantiate(lightningEffectPrefab, enemyTarget.transform.position, Quaternion.identity);
                currentProjectile.SetDamageModifier(_damageModifier);
                yield return new WaitForSeconds(0.1f);                   
            }
        }
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
            _zoneCount++;//Increase projectiles by 1 every second level
        }
        else{
            _damageModifier+= 0.2f;//Increase damage by 10% every second level
        }
    }
}