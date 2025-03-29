using UnityEngine;


public class PlayerCombatManager : CombatManager {
    private bool isLoaded = false;
    public void Initialize(float maxHealth)
    {
        if (!isLoaded)
        {
            currentMaxHealth = maxHealth;
            currentHealth = currentMaxHealth;
        }
    }
    private void Start()
    {
        if (SaveGameManager.Instance != null)
        {
            UpdateHealthBar();
        }
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
        SaveGameManager.OnSave -= OnSave;
    }
    private void OnLoad(SerializedSaveGame saveData)
    {
        Debug.Log($"Loading health: {saveData.playerHP}/{saveData.playerMaxHP}");

        currentHealth = saveData.playerHP;
        currentMaxHealth = saveData.playerMaxHP;
        isLoaded = true;
        Debug.Log($"After loading: {currentHealth}/{currentMaxHealth}");

        UpdateHealthBar();
    }
    private void OnSave(SerializedSaveGame saveData)
    {
        saveData.playerHP = currentHealth;
        saveData.playerMaxHP = currentMaxHealth;
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