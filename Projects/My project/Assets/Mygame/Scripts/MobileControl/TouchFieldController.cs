using UnityEngine;
using UnityEngine.EventSystems;

public class TouchFieldController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector2 direction { get; private set; }

    private Vector2 pointerStartPosition;
    private Vector2 positionFuong;

    public bool isPointerDown = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerStartPosition = eventData.position;
        positionFuong =new Vector2(CamManager.Instance.lookAngle,CamManager.Instance.pivotAngle);
        isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPointerDown)
        {
            direction = positionFuong + eventData.position - pointerStartPosition;
        }
    }
}
