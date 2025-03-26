using System.Collections.Generic;
using UnityEngine;


public class AbilityManager : MonoBehaviour {
    [SerializeField] private List<Ability> abilities;


    private void Update()
    {
        foreach(Ability ability in abilities){
            ability.Activate();
        }
    }
}