using UnityEngine;


public class PlayerCombatManager : CombatManager {

    public void Initialize(float maxHealth)
    {
        currentMaxHealth = maxHealth;
        currentHealth = currentMaxHealth;
    }

    private void OnEnable()
    {
        SaveGameManager.OnSave += OnSave;
        SaveGameManager.OnLoad += OnLoad;
        healthBar.SetFillAmount(currentHealth, currentMaxHealth);
    }
    private void OnDisable()
    {
        SaveGameManager.OnLoad -= OnLoad;
        SaveGameManager.OnSave -= OnLoad;
    }

    private void OnLoad(SerializedSaveGame saveData)
    {
        currentHealth = saveData.playerHP;
    }

    private void OnSave(SerializedSaveGame saveData)
    {
        saveData.playerHP = currentHealth;
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