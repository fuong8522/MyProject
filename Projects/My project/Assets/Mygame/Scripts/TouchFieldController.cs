using UnityEngine;
using UnityEngine.EventSystems;

public class TouchFieldController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector2 direction { get; private set; }

    private Vector2 pointerStartPosition;

    private bool isPointerDown = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerStartPosition = eventData.position;
        isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        direction = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPointerDown)
        {
            direction = eventData.position - pointerStartPosition;
        }
    }
}
