using UnityEngine;
using System.Collections;

public class EnemyAOEAttack : MonoBehaviour
{
    private static readonly int AOEAttack = Animator.StringToHash("AOEAttack");

    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyMovement movement;

    [SerializeField] private CombatData attackData;
    [SerializeField] TriggerHandler triggerHandler;

    [SerializeField] private GameObject attackEffect;

    private PlayerCombatManager playerCombat;

    private bool isPlayerInCollider = false;
    private bool canAttack = true;

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
        movement.isAttacking = true;
        animator.SetTrigger(AOEAttack);
    }

    public void OnAttack() //animation event
    {
        if (attackEffect)
        {
            attackEffect = Instantiate(attackEffect, transform.position, Quaternion.identity);
            Destroy(attackEffect, 1);
        }
        ApplyDamage();

    }
    public void OnAttackEnd() //animation event
    {
        movement.isAttacking = false;
        animator.ResetTrigger(AOEAttack);
    }

    public void HandleTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(collision.name);
            isPlayerInCollider = true;
            playerCombat = collision.GetComponent<PlayerCombatManager>();
            if (playerCombat == null)
            {
                Debug.Log("playercombat null");
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
