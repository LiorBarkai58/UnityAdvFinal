using UnityEngine;


public class PlayerCombatManager : CombatManager {

    public void Initialize(float maxHealth)
    {
        currentMaxHealth = maxHealth;
        currentHealth = currentMaxHealth;
    }

    public void UpdateMaxHealth(float maxHealth){
        float Difference = maxHealth - currentMaxHealth;
        RestoreHealth(Difference);//Restore the amount that was added
    }
    
}