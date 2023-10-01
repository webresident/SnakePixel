using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public delegate void SwipeEnd(Vector2 newDirection);
    public event SwipeEnd OnSwipeEnd;
    private Vector2 startPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnSwipeEnd((eventData.position - startPosition).normalized);
    }
}
