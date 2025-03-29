using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Stats {
    AttackSpeed,
    MaxHealth,
    ExpGain,
    CritChance
}

public class PlayerStats : MonoBehaviour {
     [SerializeField] private DefaultStats defaultStats; // Reference to the ScriptableObject

     [SerializeField] private StatUpgrades statUpgrades;

    private Dictionary<Stats, float> baseStats = new Dictionary<Stats, float>();

    public Dictionary<Stats, float> BaseStats => baseStats;
    private Dictionary<Stats, float> modifiers = new Dictionary<Stats, float>();

    public event UnityAction OnStatsUpdated;

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
    public float GetStatModifier(Stats stat)
    {
        return modifiers.ContainsKey(stat) ? modifiers[stat] : 0f;
    }
    public void SetModifier(Stats stat, float value)
    {
        modifiers[stat] = value;
    }
    public float GetStatValue(Stats stat)
    {
        return baseStats[stat] * (1f + modifiers[stat]);
    }

    public void AddModifier(Stats stat, float value)
    {
        modifiers[stat] += value;
        OnStatsUpdated?.Invoke();
    }

    public void RemoveModifier(Stats stat, float value)
    {
        modifiers[stat] -= value;
    }

    public void UpgradeStat(Stats stat){
        if(modifiers.ContainsKey(stat)){
            modifiers[stat] += statUpgrades.GetStatIncrease(stat);
            OnStatsUpdated?.Invoke();
        }
    }
    public void ResetModifiers()
    {
        foreach (Stats stat in System.Enum.GetValues(typeof(Stats)))
        {
            modifiers[stat] = 0f;
        }
    }
}