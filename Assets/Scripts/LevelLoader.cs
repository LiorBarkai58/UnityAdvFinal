using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private UI_ProgressBar _progressBar;
    [SerializeField] private TextMeshProUGUI progressText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public async void LoadScene(string sceneName)
    //{
    //    var scene = SceneManager.LoadSceneAsync(sceneName);
    //    scene.allowSceneActivation = false;
    //
    //    _loaderCanvas.SetActive(true);
    //
    //    do
    //    {
    //        await Task.Delay(1000); //add artificial timer because scene loads too fast to show loading screen
    //        _progressBar.SetFillAmount(scene.progress, 0.9f);
    //    }while (scene.progress < 0.9f);
    //
    //    scene.allowSceneActivation = true;
    //    _loaderCanvas.SetActive(false);
    //}


    public void LoadLevel(int sceneIndex)
    {
        if (SaveGameManager.Instance != null)
        {
            SaveGameManager.Instance.SaveGame();
        }
        StartCoroutine(LoadScene(sceneIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            _loaderCanvas.SetActive(true);
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            _progressBar.SetFillAmount(progress, 0.9f);
            progressText.SetText($"loading: {progress * 100f}%");

            yield return null;
        }
        _loaderCanvas.SetActive(false);
    }
}
