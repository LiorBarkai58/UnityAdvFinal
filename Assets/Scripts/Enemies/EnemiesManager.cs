using System.Collections.Generic;
using UnityEngine;


public class EnemiesManager : MonoBehaviour {
    public List<EnemyManager> enemies = new List<EnemyManager>();//Will be made internally later but currently uses scene references

    private int KillCounter = 0;
    void Start()
    {
        foreach(EnemyManager enemy in enemies){
            enemy.OnDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath(){
        KillCounter++;
    }
}