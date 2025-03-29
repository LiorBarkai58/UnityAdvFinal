using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemiesManager : MonoBehaviour {
    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private EnemyPool enemyPool;
    private List<EnemyManager> enemies = new List<EnemyManager>();

    [Header("Spawn Details")]
    [SerializeField] private int SpawnInterval = 10;

    [SerializeField] private int AmountToSpawn = 5;

    private int KillCounter = 0;

    public int KillCount
    {
        get => KillCounter; set { KillCounter = value; }
    }

    public event UnityAction<int> OnKillUpdated;
    void Start()
    {
        enemyPool.InitializePool();
        StartCoroutine(EnemySpawning());
    }

    private void HandleEnemyDeath(EnemyManager enemy){
        enemy.OnDeath -= HandleEnemyDeath;
        enemyPool.ReleaseEnemy(enemy);
        enemies.Remove(enemy);
        KillCounter++;
        OnKillUpdated?.Invoke(KillCounter);
    }

    private IEnumerator EnemySpawning(){
        while(true){
            for(int i = 0; i< AmountToSpawn; i++){
                EnemyManager currentEnemy = enemyPool.GetEnemy();
                enemies.Add(currentEnemy);
                currentEnemy.OnDeath += HandleEnemyDeath;
                currentEnemy.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}