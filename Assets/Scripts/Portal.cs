using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour {
    private bool used = false;
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player") && !used){
            used = true;
            LevelLoader.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}