using System.Collections.Generic;
using UnityEngine;


public class FireStaff : Ability
{
    [SerializeField] private Projectile FireBallPrefab;

    public List<CombatManager> enemiesInRange = new List<CombatManager>();
    public override bool AbilityLogic()
    {
        if(enemiesInRange.Count > 0){
            Projectile currentProjectile = Instantiate(FireBallPrefab, transform.position, Quaternion.identity);

            CombatManager enemyTarget = enemiesInRange[Random.Range(0, enemiesInRange.Count-1)];
            Vector3 direction = (enemyTarget.transform.position - transform.position).normalized;
            currentProjectile.SetDirection(direction);

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
}