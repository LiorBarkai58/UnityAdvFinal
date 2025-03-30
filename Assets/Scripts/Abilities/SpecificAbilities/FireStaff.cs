using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStaff : Ability
{
    [SerializeField] private Projectile FireBallPrefab;


    private int _ProjectileCount = 1;

    
    public override bool AbilityLogic()
    {
        if(enemiesInRange.Count > 0){
            StartCoroutine(ShootFireballs());
            return true;
        }
        else return false;
    }

    private IEnumerator ShootFireballs(){
        for(int i = 0; i < _ProjectileCount; i++){
            if(enemiesInRange.Count > 0){//Here to make sure the enemies that were in range don't die during the cast
                Projectile currentProjectile = Instantiate(FireBallPrefab, transform.position, Quaternion.identity);
                CombatManager enemyTarget = enemiesInRange[Random.Range(0, enemiesInRange.Count-1)];
                Vector3 direction = (enemyTarget.transform.position - transform.position).normalized;
                currentProjectile.SetDirection(direction);
                currentProjectile.SetDamageModifier(_damageModifier);
                yield return new WaitForSeconds(0.1f);                   
            }
        }
    }

    
    public override void UpgradeAbility()
    {
        base.UpgradeAbility();
        if(_level %2 == 0){
            _ProjectileCount++;//Increase projectiles by 1 every second level
        }
        else{
            _damageModifier+= 0.1f;//Increase damage by 10% every second level
        }
    }
}