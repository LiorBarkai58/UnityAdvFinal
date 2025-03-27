using UnityEngine;
using System.Collections;

public abstract class EnemyAttack : MonoBehaviour
{
    private static readonly int Attack = Animator.StringToHash("Attack");

    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyMovement movement;

    [SerializeField] private float attackRange = 20;
    [SerializeField] private float attackCooldown;

    private bool canAttack = true;

    private void FixedUpdate()
    {
        HandleAttack();
    }

    protected virtual void HandleAttack()
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
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    protected virtual void StartAttack()
    {
        movement.isAttacking = true;
        animator.SetTrigger(Attack);
    }
    public virtual void OnAttackEnd() //animation event
    {
        movement.isAttacking = false;
        animator.ResetTrigger(Attack);
    }
}
