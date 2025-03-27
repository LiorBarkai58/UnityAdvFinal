using System.Collections.Generic;
using UnityEngine;


public class FireStaff : Ability
{
    [SerializeField] private Projectile FireBallPrefab;

    private List<CombatManager> enemiesInRange = new List<CombatManager>();

    private int _ProjectileCount = 1;

    private float _damageModifier  = 1;

    public override bool AbilityLogic()
    {
        if(enemiesInRange.Count > 0){
            for(int i = 0; i < _ProjectileCount; i++){
                if(enemiesInRange.Count > 0){//Here to make sure the enemies that were in range don't die during the cast
                    Projectile currentProjectile = Instantiate(FireBallPrefab, transform.position, Quaternion.identity);
                    CombatManager enemyTarget = enemiesInRange[Random.Range(0, enemiesInRange.Count-1)];
                    Vector3 direction = (enemyTarget.transform.position - transform.position).normalized;
                    currentProjectile.SetDirection(direction);
                    currentProjectile.SetDamageModifier(_damageModifier);                      
                }
            }
            return true;
        }
        else return false;
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
            _ProjectileCount++;//Increase projectiles by 1 every second level
        }
        else{
            _damageModifier+= 0.1f;//Increase damage by 10% every second level
        }
    }
}