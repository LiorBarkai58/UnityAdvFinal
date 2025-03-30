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

    public PlayerCombatManager playerCombat;

    private bool isPlayerInCollider = false;
    private bool canAttack = true;

    private void FixedUpdate()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.PlayersTransform.position);

        if (distanceToPlayer <= attackData.Range && isPlayerInCollider && canAttack)
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
        movement.canMove = false;
        animator.SetTrigger(AOEAttack);
    }

    public void OnAttack() //animation event
    {
        GameObject currentAttackEffect = Instantiate(attackEffect, transform.position, Quaternion.identity);
        Destroy(currentAttackEffect, 1);
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.PlayersTransform.position);
        if (distanceToPlayer <= attackData.Range)
        {
            ApplyDamage();
        }


    }
    public void OnAttackEnd() //animation event
    {
        movement.canMove = true;
        animator.ResetTrigger(AOEAttack);
    }

    public void HandleTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCombat = collision.GetComponent<PlayerCombatManager>();
            if (playerCombat)
            {
                isPlayerInCollider = true;
            }
        }
    }

    public void HandleTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerCombatManager currentCollider = collision.gameObject.GetComponent<PlayerCombatManager>();
            if(playerCombat && currentCollider && currentCollider.gameObject == playerCombat.gameObject){
                isPlayerInCollider = false;
                playerCombat = null;
            }
            
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
