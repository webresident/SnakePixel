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
        direction.x = newDirection.x;
        direction.y = newDirection.y;
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
