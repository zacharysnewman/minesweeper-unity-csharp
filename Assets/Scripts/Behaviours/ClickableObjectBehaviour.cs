using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableObjectBehaviour : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnLeftClick;
    public UnityEvent OnMiddleClick;
    public UnityEvent OnRightClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            OnLeftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            OnMiddleClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnRightClick.Invoke();
    }
}
