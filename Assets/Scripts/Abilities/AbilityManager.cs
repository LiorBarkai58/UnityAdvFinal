using System.Collections.Generic;
using UnityEngine;


public class AbilityManager : MonoBehaviour {
    [SerializeField] private List<Ability> abilities;

    private float attackSpeedMultiplier = 1;
    private void Update()
    {
        foreach(Ability ability in abilities){
            ability.AbilityUpdate(attackSpeedMultiplier);
            ability.Activate();
        }
    }

    public void SetAttackSpeed(float AttackSpeedMultiplier){
        this.attackSpeedMultiplier = AttackSpeedMultiplier;
    }
}