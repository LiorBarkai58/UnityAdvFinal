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

        subheaderMessages = new List<string>
        {
            "don't worry, it's ok, don't feel bad, this is a safe space and we don't judge your skill issue",
            "oof, that looks like it hurt",
            "git gud scrub\n(respectfully)",
            "me when the",
            "a.k.a. the sweet release",
            "true failure comes when you stop trying",
            "the only way to win is to fight",
            "have you tried dookim yet? i hear it's fun"
        };
    }

    private void HandlePlayerDied(CombatManager combatManager)
    {

    }
    private void SetSubheaderText()
    {

    }
}
