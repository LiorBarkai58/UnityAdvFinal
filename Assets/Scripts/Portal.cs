using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour {
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            LevelLoader.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}