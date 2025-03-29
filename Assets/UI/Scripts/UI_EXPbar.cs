using TMPro;
using UnityEngine;


public class UI_EXPbar : UI_ProgressBar {
    [SerializeField] private PlayerExperience playerEXP;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        if(playerEXP){
          playerEXP.OnEXPChange += SetFillAmount;
          playerEXP.OnLevelUp += UpdateLevel;  
        } 
        else Debug.LogWarning("Missing playerEXP reference in killcounter");

        
    }

    private void UpdateLevel(){
        if(levelText) levelText.SetText($"Lvl.{playerEXP.Level}");
    }
}