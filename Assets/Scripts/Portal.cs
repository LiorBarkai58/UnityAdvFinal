using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour {
    [SerializeField] private LevelLoader levelLoader;
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}