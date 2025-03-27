using UnityEngine;
using System.Collections;

public class EnemyProjectileAttack : MonoBehaviour
{
    private static readonly int Attack = Animator.StringToHash("Attack");

    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyProjectile FireBallPrefab;

    [SerializeField] private float attackRange = 20;
    [SerializeField] private float attackCooldown;

    private bool canAttack = true;

    private void FixedUpdate()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.PlayersTransform.position);

        if (distanceToPlayer <= attackRange && canAttack)
        {
            canAttack = false;
            StartAttack();
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(10);
        canAttack = true;
    }


    private void StartAttack()
    {
        // animator.SetTrigger(Attack);
        EnemyProjectile currentProjectile = Instantiate(FireBallPrefab, transform.position, Quaternion.identity);
        Vector3 direction = (playerTransform.PlayersTransform.position - transform.position).normalized;
        currentProjectile.SetDirection(direction);
      //  animator.ResetTrigger(Attack);
    }

}
