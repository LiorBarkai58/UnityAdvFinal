using System;
using System.Collections.Generic;
using UnityEngine;

public enum Stats {
    AttackSpeed,
    MaxHealth,
    ExpGain,
    CritChance
}

public class PlayerStats : MonoBehaviour {
     [SerializeField] private DefaultStats defaultStats; // Reference to the ScriptableObject

    private Dictionary<Stats, float> baseStats = new Dictionary<Stats, float>();
    private Dictionary<Stats, float> modifiers = new Dictionary<Stats, float>();

    public void Initialize()
    {
        if (defaultStats == null)
        {
            Debug.LogError("No default stats referenced");
            return;
        }

        //Initialize default stats
        baseStats[Stats.AttackSpeed] = defaultStats.AttackSpeed;
        baseStats[Stats.MaxHealth] = defaultStats.MaxHealth;
        baseStats[Stats.ExpGain] = defaultStats.ExpGain;
        baseStats[Stats.CritChance] = defaultStats.CritChance;

        //Set modifiers to 0 for later use
        foreach (Stats stat in Enum.GetValues(typeof(Stats)))
        {
            modifiers[stat] = 0f;
        }
    }

    public float GetStatValue(Stats stat)
    {
        return baseStats[stat] * (1f + modifiers[stat]);
    }

    public void AddModifier(Stats stat, float value)
    {
        modifiers[stat] += value;
    }

    public void RemoveModifier(Stats stat, float value)
    {
        modifiers[stat] -= value;
    }
}