using System.Collections.Generic;
using UnityEngine;


public class EnemyPool : MonoBehaviour {
    [SerializeField] private EnemyManager EnemyPrefab;

    [SerializeField] private int AmountToPool = 10;

    private Queue<EnemyManager> enemyPool = new Queue<EnemyManager>();

    public void InitializePool()
    {
        for(int i = 0; i < AmountToPool; i++){
            EnemyManager currentEnemy = Instantiate(EnemyPrefab, transform);
            currentEnemy.gameObject.SetActive(false);
            enemyPool.Enqueue(currentEnemy);
        }
    }

    public EnemyManager GetEnemy(){
        if(enemyPool.Count > 0){
            return enemyPool.Dequeue();
        }
        return Instantiate(EnemyPrefab, transform);
    }

    public void ReleaseEnemy(EnemyManager enemy){
        enemy.gameObject.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}