using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private PlayerCombatManager player;
    [SerializeField] private TextMeshProUGUI subheaderTMP;
    private List<string> subheaderMessages;
    

    private void OnValidate()
    {
        player.OnDeath += HandlePlayerDied;
    }

    private void HandlePlayerDied(CombatManager combatManager)
    {

    }
    private void SetSubheaderText()
    {

    }
}
