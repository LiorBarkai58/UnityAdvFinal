using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour {
    [SerializeField] private PlayerExperience playerExperience;
    public event UnityAction OnPlayerInteract;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnPlayerInteract?.Invoke();
        }
    }

    public void OnLevelupShrine(){
        playerExperience.ShrineLevelUp();
    }
}