using System.Collections.Generic;
using UnityEngine;


public class EnemiesManager : MonoBehaviour {
    public List<EnemyCombatManager> enemiesCombat = new List<EnemyCombatManager>();//Will be made internally later but currently uses scene references

    private int KillCounter = 0;
    void Start()
    {
        foreach(EnemyCombatManager enemy in enemiesCombat){
            enemy.OnDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath(CombatManager combatManager){
        KillCounter++;
    }
}