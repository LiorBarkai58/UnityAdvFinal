using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] private EnemyAOEAttack enemyAttack;

    private void OnTriggerEnter(Collider collision)
    {
        enemyAttack?.HandleTriggerEnter(collision);
    }

    private void OnTriggerExit(Collider collision)
    {
        enemyAttack?.HandleTriggerExit(collision);
    }
}
