using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class BootMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference anyPressAction;

    private void OnEnable()
    {
        anyPressAction.action.performed += OnAnyPressed;
        anyPressAction.action.Enable();
    }

    private void OnDisable()
    {
        anyPressAction.action.performed -= OnAnyPressed;
        anyPressAction.action.Disable();
    }

    private void OnAnyPressed(InputAction.CallbackContext ctx)
    {
        SceneController.instance.ChangeScene("MainMenu");
    }
}
