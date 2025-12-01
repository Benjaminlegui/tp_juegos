using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    void Start()
    {
        if (GetCurrentLevelName() == "Level01")
        {
            AudioManager.instance.PlayBGM("music");
        }
    }

    void Update()
    {
        ReturnToMainMenu();
    }

    private void ReturnToMainMenu()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            AudioManager.instance.StopBGM();
            SceneController.instance.ChangeScene("MainMenu");
        }
    }

    public string GetCurrentLevelName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
