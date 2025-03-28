using UnityEngine;


public class EnemyCombatManager : CombatManager {
    [SerializeField] private CombatData combatData;
    [SerializeField] private TextMesh damageNumberPrefab;

    private void OnEnable(){
        currentMaxHealth = combatData.MaxHealth;
        currentHealth = combatData.MaxHealth;
    }

    public override void TakeDamage(DamageArgs damageArgs)
    {
        base.TakeDamage(damageArgs);
        if (damageNumberPrefab)
        {
            SpawnDamageText(damageArgs);
        }
    }
    private void SpawnDamageText(DamageArgs damageArgs)
    {
        TextMesh damagePrefabClone = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity, transform);
        damagePrefabClone.text = damageArgs.Damage.ToString();
    }
}