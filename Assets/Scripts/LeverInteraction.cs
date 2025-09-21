using UnityEngine;
using UnityEngine.InputSystem;

public class LeverInteraction : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject hiddenPlatform;
    private bool playerInteracting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (interactAction)
        {
            interactAction.action.performed += OnInteractPerformed;
            interactAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (interactAction)
        {
            interactAction.action.performed -= OnInteractPerformed;
            interactAction.action.Disable();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == player)
        {
            playerInteracting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == player)
        {
            playerInteracting = false;
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
        if (!playerInteracting) return; // Only when player is interacting
        if (!animator) return; // Check if animator exists

        animator.SetTrigger("Activate");
        hiddenPlatform.SetActive(true);
    }
}
