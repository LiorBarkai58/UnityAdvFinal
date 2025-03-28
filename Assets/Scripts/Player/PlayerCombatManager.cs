using UnityEngine;


public class PlayerCombatManager : CombatManager {

    public void Initialize(float maxHealth)
    {
        currentMaxHealth = maxHealth;
        currentHealth = currentMaxHealth;
    }

    private void OnEnable()
    {
        healthBar.SetFillAmount(currentHealth, currentMaxHealth);
    }

    public void UpdateMaxHealth(float maxHealth){
        float Difference = maxHealth - currentMaxHealth;
        RestoreHealth(Difference);//Restore the amount that was added
        healthBar.SetFillAmount(currentHealth, currentMaxHealth);
    }

    public override void TakeDamage(DamageArgs damageArgs)
    {
        base.TakeDamage(damageArgs);
    }
}