using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool Pressed;

        public Button.ButtonClickedEvent OnPointerDownEvent = new Button.ButtonClickedEvent();
        public Button.ButtonClickedEvent OnPointerUpEvent = new Button.ButtonClickedEvent();
            
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent.Invoke();
            Pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpEvent.Invoke();
            Pressed = false;
        }
    }
}