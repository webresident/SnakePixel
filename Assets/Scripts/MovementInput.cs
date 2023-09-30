using UnityEngine;

[System.Serializable]
public enum Direction
{
    Up = 1 << 0,
    Right = 1 << 1,
    Down = 1 << 2,
    Left = 1 << 3,
}
public class MovementInput : MonoBehaviour
{
    public delegate void ButttonClick(Vector2 newDirection);
    public event ButttonClick OnButtonClicked;
    public void OnMovementButtonClicked(Direction direction)
    {
        Vector2 result;
        switch (direction)
        {
            case Direction.Up:
                result = new Vector2(0f, 1f);
                break;
            case Direction.Right:
                result = new Vector2(1f, 0f);
                break;
            case Direction.Down:
                result = new Vector2(0f, -1f);
                break;
            case Direction.Left:
                result = new Vector2(-1f, 0);
                break;
            default:
                result = new Vector2(0, 0);
                break;
        }
        OnButtonClicked(result);
    }
}
