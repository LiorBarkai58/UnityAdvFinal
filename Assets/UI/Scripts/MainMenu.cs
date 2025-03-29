using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameButton()
    {
        LevelLoader.Instance.LoadLevel(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
