using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool Pressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }
    }
}