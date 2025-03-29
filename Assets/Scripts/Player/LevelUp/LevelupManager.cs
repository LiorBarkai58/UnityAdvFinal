using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelupManager : MonoBehaviour {


    [SerializeField] private List<AbilityCard> abilityCards;

    [SerializeField] private List<StatCard> statCards;


    public event UnityAction<Stats> OnStatUpgrade;
    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Start()
    {
        foreach(AbilityCard card in abilityCards){
            card.OnSelected += HandleAbilityUpgrade;
        }   
        foreach(StatCard card in statCards){
            card.OnSelected += HandleStatUpgrade;
        }
    }

    public void SetupLevel(List<Ability> abilities, List<Stats> stats){

        List<Ability> AbilitiesCopy = new List<Ability>(abilities);

        List<Stats> StatsCopy = new List<Stats>(stats);

        for(int i = 0; i< Mathf.Min(2, abilities.Count); i++){
            Ability selectedAbility = AbilitiesCopy[Random.Range(0, AbilitiesCopy.Count)];

            AbilitiesCopy.Remove(selectedAbility);

            if(i < abilityCards.Count){
                Debug.Log("test");
                abilityCards[i].SetAbility(selectedAbility);
            }

        }
        for(int i = 0; i< Mathf.Min(2, stats.Count); i++){
            Stats selectedStat = StatsCopy[Random.Range(0, StatsCopy.Count)];

            StatsCopy.Remove(selectedStat);
            
            if(i < statCards.Count){
                statCards[i].SetStat(selectedStat);
            }
        }

        
    }

    private void HandleAbilityUpgrade(Ability ability){
        ability.UpgradeAbility();
    }

    private void HandleStatUpgrade(Stats stat){
        OnStatUpgrade?.Invoke(stat);
    }




    

    
}