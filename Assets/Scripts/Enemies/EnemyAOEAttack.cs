using UnityEngine;
public class EnemyAOEAttack : EnemyAttack
{
    [SerializeField] private CombatData attackData;
    private CombatManager playerCombat;

    private bool isPlayerInCollider = false;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }

        Rigidbody parentRigidbody = GetComponentInParent<Rigidbody>();
        if (parentRigidbody == null)
        {
            Debug.LogWarning("No Rigidbody found on parent. Trigger might not work correctly.");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"Trigger Enter: {collision.gameObject.name}, Tag: {collision.tag}");
        Debug.Log($"Children of {collision.gameObject.name}:");
        foreach (Transform child in collision.transform)
        {
            Debug.Log(child.name);
        }
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player in collider");
            isPlayerInCollider = true;
            playerCombat = collision.GetComponent<CombatManager>();

            if (playerCombat == null)
            {
                Debug.LogError("No CombatManager found on player!");
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player out of collider");
            isPlayerInCollider = false;
            playerCombat = null;
            
        }
    }

    private void ApplyDamage()
    {
        Debug.Log($"Attempting to apply damage. Player in Collider: {isPlayerInCollider}, PlayerCombat: {playerCombat != null}");
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
