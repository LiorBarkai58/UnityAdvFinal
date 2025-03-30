using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RuneCircle : Ability
{

    [SerializeField] private float Damage;

    private void Start()
    {
        _level = 1;
    }
    public override bool AbilityLogic()
    {
        if(enemiesInRange.Count == 0) return false;

        // Create a copy of the list to iterate over, different abilities can kill an enemy during the iteration and change the original list
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (enemiesInRange[i] != null)
                enemiesInRange[i].TakeDamage(new DamageArgs { Damage = this.Damage * _damageModifier });
        }
        return true;
    }

    public override void UpgradeAbility()
    {
        base.UpgradeAbility();
        if(_level %2 == 0){
            Vector3 endValue = this.transform.localScale * 1.15f;
            endValue.y = 1;
            transform.DOScale(endValue, 0.3f);//Increase scale by 15% per second level
        }
        else{
            _damageModifier+= 0.1f;//Increase damage by 10% every second level
        }
    }
}