using System.Collections.Generic;
using UnityEngine;


public class FireStaff : Ability
{
    [SerializeField] private Projectile FireBallPrefab;

    public List<CombatManager> enemiesInRange = new List<CombatManager>();
    public override bool AbilityLogic()
    {
        Debug.Log($"Activated");
        if(enemiesInRange.Count > 0){
            Projectile currentProjectile = Instantiate(FireBallPrefab, transform.position, Quaternion.identity);

            CombatManager enemyTarget = enemiesInRange[Random.Range(0, enemiesInRange.Count-1)];
            Vector3 direction = (enemyTarget.transform.position - transform.position).normalized;
            currentProjectile.SetDirection(direction);

            return true;
        }
        else return false;
    }

    void OnTiggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")){
            CombatManager enemyCombat = other.GetComponent<CombatManager>();
            if(enemyCombat){
                enemiesInRange.Add(enemyCombat);
            }
        }
    }
    void OnTiggerExit(Collider other)
    {
        if(other.CompareTag("Enemy")){
            CombatManager enemyCombat = other.GetComponent<CombatManager>();
            if(enemyCombat && enemiesInRange.Contains(enemyCombat)){
                enemiesInRange.Remove(enemyCombat);
            }
        }
    }
}