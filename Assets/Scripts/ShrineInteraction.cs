using UnityEngine;

public class ShrineInteraction : MonoBehaviour
{
    [SerializeField] private GameObject frogModel;
    [SerializeField] private GameObject interationPrompt;
    public PlayerInteractions playerInteractions;
    private bool isInteractRadius;

    private bool used = false;

    private void HandleInteraction()
    {
        if (isInteractRadius && !used && playerInteractions)
        {
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
            
            PlayerInteractions current = other.gameObject.GetComponent<PlayerInteractions>();

            if (current)
            {
                isInteractRadius = true;
                interationPrompt.SetActive(true);
                playerInteractions = current;
                playerInteractions.OnPlayerInteract += HandleInteraction;

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !used)
        {
            PlayerInteractions current = other.gameObject.GetComponent<PlayerInteractions>();
            if(playerInteractions == current){
                isInteractRadius = false;
                if(interationPrompt) interationPrompt.SetActive(false);
                playerInteractions.OnPlayerInteract -= HandleInteraction;
                playerInteractions = null;
            }
        }
    }
}
