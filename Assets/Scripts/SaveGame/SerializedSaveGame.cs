using System.Collections.Generic;
using UnityEngine;
public class SerializedSaveGame
{
    public float playerHP;
    public float playerMaxHP;
    public float playerPositionX, playerPositionY, playerPositionZ;
    public float playerRotationX, playerRotationY, playerRotationZ;
    public int killCount;
    public float timer;
    public float currentEXP;
    public int level;
    public float EnemyTimer; 

    public List<SerializedAbility> abilities = new List<SerializedAbility>();
    public List<SerializedStat> stats = new List<SerializedStat>();
}
[System.Serializable]
public class SerializedAbility
{
    public int abilityID;
    public int abilityLevel;
}
[System.Serializable]
public class SerializedStat
{
    public Stats statType;
    public float statModifier;
}