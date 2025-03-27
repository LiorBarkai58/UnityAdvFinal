using UnityEngine;
public class EnemyAOEAttack : EnemyAttack
{
    [SerializeField] private CombatData attackData;
    [SerializeField] private CombatManager playerCombat;
    [SerializeField] TriggerHandler triggerHandler;

    private bool isPlayerInCollider = false;


    public void HandleTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            isPlayerInCollider = true;
        }
    }

    public void HandleTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInCollider = false;
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

    public override void OnAttackEnd()
    {
        base.OnAttackEnd(); 
        ApplyDamage();
    }
}
