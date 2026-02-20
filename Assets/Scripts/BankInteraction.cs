using UnityEngine;

public class BankInteraction : MonoBehaviour
{
    public GameObject talkUI;
    public LoanSystem loanSystem;
    private bool playerInside = false;
    private PlayerMovement playerMovement;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            StartConversation();
        }
    }

    void StartConversation()
    {
        if (playerMovement != null)
        {
            loanSystem.StartLoanConversation(playerMovement);
        }

        talkUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            talkUI.SetActive(true);

            playerMovement = other.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            talkUI.SetActive(false);
        }
    }
}