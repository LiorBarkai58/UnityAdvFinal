using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private PlayerCombatManager player;
    [SerializeField] private TextMeshProUGUI subheaderTMP;
    [SerializeField] private GameObject hudCanvas;
    private List<string> subheaderMessages;
    

    private void Awake()
    {
        player.OnDeath += HandlePlayerDied;

        subheaderMessages = new List<string>
        {
            "don't worry, it's ok, don't feel bad, this is a safe space and we don't judge your skill issue",
            "oof, that looks like it hurt",
            "git gud scrub\n(respectfully)",
            "me when the"
        };
    }

    private void HandlePlayerDied(CombatManager combatManager)
    {
        hudCanvas.SetActive(false);
        Time.timeScale = 0f;
        SetSubheaderText();
    }
    private void SetSubheaderText()
    {
        string currentDeathMessage = subheaderMessages[Random.Range(0, subheaderMessages.Count)];
        subheaderTMP.SetText(currentDeathMessage);
    }

    public void RetryButton()
    {
        hudCanvas.SetActive(true);
        Time.timeScale = 1f;
        LevelLoader.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenuButton()
    {
        hudCanvas.SetActive(true);
        Time.timeScale = 1f;
        LevelLoader.Instance.LoadLevel(0);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
