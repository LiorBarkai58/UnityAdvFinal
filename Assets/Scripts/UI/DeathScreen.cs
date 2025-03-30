using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private PlayerCombatManager player;
    [SerializeField] private EnemiesManager enemiesManager;

    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject deathCanvas;

    [Header("Hierarchy references")]

    [SerializeField] private TextMeshProUGUI KillCounterText;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private TextMeshProUGUI subheaderTMP;

    private List<string> subheaderMessages;
    

    private void Awake()
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
        Debug.Log("player died");
        hudCanvas.SetActive(false);
        deathCanvas.SetActive(true);
        KillCounterText.SetText(enemiesManager.KillCount.ToString());
        TimerText.SetText(string.Format("{0:00}:{1:00}", enemyPool.Timer/60, enemyPool.Timer%60));
        
        SaveGameManager.Instance.DeleteSaveData();
        Time.timeScale = 0f;
        SetSubheaderText();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void SetSubheaderText()
    {
        string currentDeathMessage = subheaderMessages[Random.Range(0, subheaderMessages.Count)];
        subheaderTMP.SetText(currentDeathMessage);
    }

    public void RetryButton()
    {
        deathCanvas.SetActive(false);
        hudCanvas.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
        LevelLoader.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenuButton()
    {
        deathCanvas.SetActive(false);
        hudCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 1f;
        LevelLoader.Instance.LoadLevel(0);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}