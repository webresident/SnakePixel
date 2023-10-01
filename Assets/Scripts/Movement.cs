using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float delayTime = 1f;
    private Transform currentTransform;
    private Vector3 direction;
    private Rigidbody2D rb;

    private void Start()
    {
        currentTransform = transform;
        direction = currentTransform.right;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RepeatMove());
    }
 
    public void ChangeDirection(Vector2 newDirection)
    {
        Vector2 temp = GetStraightDirection(newDirection);
        if (direction.x == -temp.x || direction.y == -temp.y)
            return;
        direction.x = temp.x;
        direction.y = temp.y;
    }

    private Vector2 GetStraightDirection(Vector2 input)
    {
        Vector2 output;
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            output = new Vector2(Mathf.Sign(input.x), 0f);
        else
            output = new Vector2(0f, Mathf.Sign(input.y));

        return output;
    }

    private IEnumerator RepeatMove()
    {
        yield return new WaitForSecondsRealtime(1f);
        while (true)
        {
            Move();
            yield return new WaitForSecondsRealtime(delayTime);
        }
    }

    private void Move()
    {
        Vector2 targetPosition = rb.position + new Vector2(direction.x, direction.y);
        rb.MovePosition(targetPosition);
    }
}
