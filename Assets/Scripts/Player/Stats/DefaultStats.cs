using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStats", menuName = "Stats/DefaultStats")]
public class DefaultStats : ScriptableObject
{
    [Tooltip("Rate of abilities recharge rate")]
    public float AttackSpeed = 1.0f;
    public float MaxHealth = 100f;
    public float ExpGain = 1.0f;
    public float CritChance = 5f;
}