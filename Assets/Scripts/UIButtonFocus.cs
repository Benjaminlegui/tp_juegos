using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonFocus : MonoBehaviour
{
    [SerializeField] private GameObject buttonToSetFocus;
    private void OnEnable()
    {
        if (buttonToSetFocus)
        {
            EventSystem.current.SetSelectedGameObject(buttonToSetFocus);
        }
    }
}
