using UnityEngine;


public class EnemyCombatManager : CombatManager {
    [SerializeField] private CombatData combatData;
    [SerializeField] private TextMesh damageNumberPrefab;

    [SerializeField] private ExperienceShard experienceShardPrefab;

    public void Initialize(float HealthMultiplier = 1){
        currentMaxHealth = combatData.MaxHealth * HealthMultiplier;
        currentHealth = combatData.MaxHealth * HealthMultiplier;
        UpdateHealthBar();

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
    protected override void HandleDeath()
    {
        base.HandleDeath();
        ExperienceShard currentShard = Instantiate(experienceShardPrefab, transform.position, Quaternion.identity);
        currentShard.SetEXP(combatData.EXPDrop);
    }
}