using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        AudioManager.instance.PlayBGM("music");
        SceneController.instance.ChangeScene("Level01");
    }

    public void ShowCredits()
    {
        Debug.Log("Show Credits");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
