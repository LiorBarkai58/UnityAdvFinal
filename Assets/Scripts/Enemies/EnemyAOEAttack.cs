using UnityEngine;
using System.Collections;

public class EnemyAOEAttack : MonoBehaviour
{
    private static readonly int Attack = Animator.StringToHash("Attack");

    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyMovement movement;

    [SerializeField] private CombatData attackData;
    [SerializeField] TriggerHandler triggerHandler;

    [SerializeField] private float attackRange = 20;
    [SerializeField] private float attackCooldown;


    private CombatManager playerCombat;

    private bool isPlayerInCollider = false;
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
        movement.isAttacking = true;
        animator.SetTrigger(Attack);
    }

    public void OnAttackEnd() //animation event
    {
        Debug.Log("Attackendevent");
        ApplyDamage();
        movement.isAttacking = false;
        animator.ResetTrigger(Attack);
    }

    public void HandleTriggerEnter(Collider collision)
    {
        Debug.Log(collision.tag);
        Debug.Log(collision.gameObject);
        if (collision.CompareTag("Player"))
        {
            Debug.Log(collision);
            isPlayerInCollider = true;
            playerCombat = collision.GetComponent<CombatManager>();
            if (playerCombat == null)
            {
                Debug.Log("playercombat null");
            }
            else
            {
                Debug.Log(playerCombat);
            }
        }
    }

    public void HandleTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInCollider = false;
            playerCombat = null;
        }
    }

    private void ApplyDamage()
    {
        if (playerCombat && isPlayerInCollider)
        {
            Debug.Log($"Applying damage: {attackData.BaseDamage}");
            playerCombat.TakeDamage(new DamageArgs { Damage = attackData.BaseDamage });
        }
    }

}
