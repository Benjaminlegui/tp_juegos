using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    private readonly string buttonSelected = "ui_button_selected";
    private readonly string buttonSubmitted = "ui_button_submitted";
    public void OnSelect(BaseEventData eventData)
    {
        AudioManager.instance.PlayUISFX(buttonSelected);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        AudioManager.instance.PlayUISFX(buttonSubmitted);
    }
}
