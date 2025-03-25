using UnityEngine;



public abstract class Ability : MonoBehaviour {

    [SerializeField] private float Cooldown;
    private float elapsedCooldown = 0;

    private bool onCooldown = false;


    private void Update()
    {
        if(onCooldown){
            elapsedCooldown += Time.deltaTime;
            if(elapsedCooldown > Cooldown){
                onCooldown = false;
                elapsedCooldown = 0;
            }
        }   
    }
    public virtual void Activate(){
        if(!onCooldown){
            AbilityLogic();
            onCooldown = true;
        }
    }

    public abstract void AbilityLogic();



}