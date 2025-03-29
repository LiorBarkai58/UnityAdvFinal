using UnityEngine;

public abstract class Ability : MonoBehaviour {

    [SerializeField] private float Cooldown;
    private float elapsedCooldown = 0;

    private bool onCooldown = false;

    public int AbilityID => GetType().Name.GetHashCode();

    protected int _level = 1;

    public int Level => _level;

    protected float _damageModifier = 1;

    protected float _personalAttackSpeedMultiplier = 1;


    //Attackspeed multiplier is a way to reduce the cooldown and increase the recharge rate of abilities
    public void AbilityUpdate(float AttackSpeedMultiplier = 1)//Ran by the ability manager and no personal update function
    {
        if(onCooldown){
            elapsedCooldown += Time.deltaTime * AttackSpeedMultiplier * _personalAttackSpeedMultiplier;
            if(elapsedCooldown > Cooldown){
                onCooldown = false;
                elapsedCooldown = 0;
            }
        }   
    }
    public virtual void Activate(){
        if(!onCooldown){
            if(AbilityLogic()){
                onCooldown = true;
            }
        }
    }

    public abstract bool AbilityLogic();//returns true if the ability was used successfully

    public virtual void UpgradeAbility(){
        _level++;
    }

    public virtual void ResetLevel()
    {
        _level = 1;
    }

}