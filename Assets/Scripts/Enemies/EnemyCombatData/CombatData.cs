using UnityEngine;

[CreateAssetMenu(menuName = "Character/CombatData")]
public class CombatData : ScriptableObject {
    [SerializeField] private float maxHealth;

    [SerializeField] private float baseDamage;

    [SerializeField] private bool takeKnockback = false;

    [SerializeField] private float cooldown;

    [SerializeField] private float range;

    [SerializeField] private float expDrop = 5;


    public float Range => range;
    public float Cooldown => cooldown;

    public float MaxHealth => maxHealth;

    public float BaseDamage => baseDamage;

    public bool TakeKnockback => takeKnockback;

    public float EXPDrop => expDrop;
}