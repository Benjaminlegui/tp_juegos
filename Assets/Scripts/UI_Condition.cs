using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UI_Condition : MonoBehaviour
{
    void Update()
    {
        ReturnToMainMenu();
    }

    private void ReturnToMainMenu()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneController.instance.ChangeScene("MainMenu");
        }
    }
}
