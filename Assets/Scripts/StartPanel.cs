using UnityEngine;
using UnityEngine.InputSystem;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject startOverlay;
    [SerializeField] private PlayerMovement playerMovement;

    private bool gameStarted = false;

    void Start()
    {
        // Pausa inicial
        Time.timeScale = 0f;
        startOverlay.SetActive(true);
        playerMovement.enabled = false;
    }

    void Update()
    {
        if (!gameStarted && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f;
        startOverlay.SetActive(false);
        playerMovement.enabled = true;
    }
}