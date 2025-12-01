using UnityEngine;

public class BottleScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController.instance.ChangeScene("WinRoom");
        }
    }
}
