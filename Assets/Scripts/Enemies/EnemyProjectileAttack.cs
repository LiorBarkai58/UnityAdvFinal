using UnityEngine;
using System.Collections;

public class EnemyProjectileAttack : MonoBehaviour
{
    private static readonly int IsMirrored = Animator.StringToHash("IsMirrored");
    private static readonly int ProjectileAttack = Animator.StringToHash("ProjectileAttack");

    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyProjectile FireBallPrefab;
    [SerializeField] private CombatData attackData;
    [SerializeField] private Transform projectilePos;

    private bool canAttack = true;
    private bool isMirrored;

    private void FixedUpdate()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.PlayersTransform.position);

        if (distanceToPlayer <= attackData.Range && canAttack)
        {
            canAttack = false;
            StartAttack();
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackData.Cooldown);
        canAttack = true;
    }


    private void StartAttack()
    {
        isMirrored = !isMirrored;
        animator.SetBool(IsMirrored, isMirrored);
        animator.SetTrigger(ProjectileAttack);
    }

    public void OnAttack()
    {
        EnemyProjectile currentProjectile = Instantiate(FireBallPrefab, projectilePos.position, Quaternion.identity);
        Vector3 direction = (playerTransform.PlayersTransform.position - transform.position).normalized;
        currentProjectile.SetDirection(direction);
        animator.ResetTrigger(ProjectileAttack);
    }

}
