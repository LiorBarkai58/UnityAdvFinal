using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemiesManager : MonoBehaviour {
    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private GameObject explosion1;
    [SerializeField] private GameObject explosion2;
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

        if (Random.value <= 0.4f)
        {
            GameObject explosionPrefab = Random.value < 0.5f ? explosion1 : explosion2;
            GameObject explosion = Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
            Destroy(explosion, 3f);
        }
    }

    private IEnumerator EnemySpawning(){
        while(true){
            for(int i = 0; i< AmountToSpawn; i++){
                EnemyManager currentEnemy = enemyPool.GetEnemy();
                enemies.Add(currentEnemy);
                currentEnemy.OnDeath += HandleEnemyDeath;
                currentEnemy.transform.position = GetSpawnPosition();
                currentEnemy.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float spawnRadius = 30f; // range around the player
        float spawnHeight = 20f; // spawn height above the player

        float angle = Random.Range(0, Mathf.PI * 2);
        float xOffset = Mathf.Cos(angle) * spawnRadius;
        float zOffset = Mathf.Sin(angle) * spawnRadius;

        Vector3 playerPos = playerTransform.PlayersTransform.position;
        return new Vector3(playerPos.x + xOffset, playerPos.y + spawnHeight, playerPos.z + zOffset);
    }
}