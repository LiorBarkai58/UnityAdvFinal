using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZoneGenerator : Ability
{
    [SerializeField] private DamageZone zonePrefab;

    private int _zoneCount = 1;


    public override bool AbilityLogic()
    {
        if(enemiesInRange.Count > 0){
            StartCoroutine(CreateZone());
            return true;
        }
        else return false;
    }

    private IEnumerator CreateZone(){
        for(int i = 0; i < _zoneCount; i++){
            if(enemiesInRange.Count > 0){//Here to make sure the enemies that were in range don't die during the cast
                CombatManager enemyTarget = enemiesInRange[Random.Range(0, enemiesInRange.Count-1)];
                DamageZone currentProjectile = Instantiate(zonePrefab, enemyTarget.transform.position, Quaternion.identity);
                currentProjectile.SetDamageModifier(_damageModifier);
                yield return new WaitForSeconds(0.1f);                   
            }
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