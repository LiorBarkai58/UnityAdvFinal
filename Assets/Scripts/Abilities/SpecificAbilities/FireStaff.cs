using System.Collections.Generic;
using UnityEngine;


public class FireStaff : Ability
{
    [SerializeField] private Projectile FireBallPrefab;

    private List<CombatManager> enemiesInRange = new List<CombatManager>();
    public override void AbilityLogic()
    {
        throw new System.NotImplementedException();
    }
}