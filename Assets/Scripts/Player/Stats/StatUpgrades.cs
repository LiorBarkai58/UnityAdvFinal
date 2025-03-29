using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Stat Upgrades", menuName = "Stats/Stat Upgrades")]
public class StatUpgrades : ScriptableObject
{
    public List<StatUpgrade> upgradesList;

    public float GetStatIncrease(Stats stat)
    {
        foreach (var upgrade in upgradesList)
        {
            if (upgrade.stat == stat)
            {
                return upgrade.increaseValue;
            }
        }

        return 0f;
    }
}
