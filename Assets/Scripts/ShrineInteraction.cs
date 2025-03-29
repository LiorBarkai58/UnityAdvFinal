using UnityEngine;

public class ShrineInteraction : MonoBehaviour
{
    [SerializeField] private GameObject frogModel;
    [SerializeField] private GameObject interationPrompt;
    private PlayerInteractions playerInteractions;
    private bool isInteractRadius;

    private bool used = false;

    private void HandleInteraction()
    {
        if (isInteractRadius && !used)
        {
            Debug.Log("interaction event triggered");
            playerInteractions.OnLevelupShrine();
            interationPrompt.SetActive(false);
            used = true;
            playerInteractions.OnPlayerInteract -= HandleInteraction;

            Destroy(frogModel);
            Destroy(interationPrompt);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !used)
        {
            isInteractRadius = true;
            interationPrompt.SetActive(true);
            playerInteractions = other.gameObject.GetComponent<PlayerInteractions>();

            if (playerInteractions)
            {
                playerInteractions.OnPlayerInteract += HandleInteraction;

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !used)
        {
            isInteractRadius = false;
            Debug.Log("player left interaction radius");
            if(interationPrompt) interationPrompt.SetActive(false);
            if(playerInteractions){
                playerInteractions.OnPlayerInteract -= HandleInteraction;
            }
        }
    }
}
