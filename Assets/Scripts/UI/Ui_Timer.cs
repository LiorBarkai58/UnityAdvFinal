using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public   TextMeshProUGUI _timerText;
    private float _elapsedTime;

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_elapsedTime / 60);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60);
        _timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    private void OnEnable()
    {
        SaveGameManager.OnSave += SaveTime;
        SaveGameManager.OnLoad += LoadTime;
    }

    private void OnDisable()
    {
        SaveGameManager.OnLoad -= LoadTime;
        SaveGameManager.OnSave -= SaveTime;
    }

    private void SaveTime(SerializedSaveGame saveData)
    {
        saveData.timer = _elapsedTime;
    }

    private void LoadTime(SerializedSaveGame saveData)
    {
        _elapsedTime = saveData.timer;
    }
}
