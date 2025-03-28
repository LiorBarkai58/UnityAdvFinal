using UnityEngine;

public class ShrineInteraction : MonoBehaviour
{
    [SerializeField] private GameObject frogModel;
    [SerializeField] private GameObject interationPrompt;
    private PlayerController playerController;
    private bool isInteractRadius;

    private void HandleInteraction()
    {
        if (isInteractRadius)
        {
            Debug.Log("interaction event triggered");
            //level up event
            interationPrompt.SetActive(false);
            Destroy(frogModel);
            Destroy(interationPrompt);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractRadius = true;
            interationPrompt.SetActive(true);
            playerController = other.GetComponentInParent<PlayerController>();

            if (playerController)
            {
                Debug.Log($"player controller set to {playerController}");
            }

            Debug.Log($"player entered interaction radius {isInteractRadius}");
            playerController.OnPlayerInteract += HandleInteraction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractRadius = false;
            Debug.Log("player left interaction radius");
            interationPrompt.SetActive(false);
            playerController.OnPlayerInteract -= HandleInteraction;
        }
    }
}
