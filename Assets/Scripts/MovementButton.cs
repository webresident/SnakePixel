using UnityEngine;
using UnityEngine.EventSystems;


public class MovementButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Direction direction = Direction.Right;
    [SerializeField] private MovementInput movementInput;
    public void OnPointerClick(PointerEventData eventData)
    {
        movementInput?.OnMovementButtonClicked(direction);
    }
}
