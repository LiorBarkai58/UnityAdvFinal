using UnityEngine;



public abstract class Ability : MonoBehaviour {

    [SerializeField] private float Cooldown;
    private float elapsedCooldown = 0;

    private bool onCooldown = false;

    //Attackspeed multiplier is a way to reduce the cooldown and increase the recharge rate of abilities
    public void AbilityUpdate(float AttackSpeedMultiplier = 1)//Ran by the ability manager and no personal update function
    {
        if(onCooldown){
            elapsedCooldown += Time.deltaTime * AttackSpeedMultiplier;
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



}