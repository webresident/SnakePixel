using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BodySegmentGenerator))]
[RequireComponent(typeof(Rotation))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float delayTime = 0.3f;
    [SerializeField] private BodySegmentGenerator bodySegmentGenerator;
    [SerializeField] private LayerMask ignoreMask;

    private Transform currentTransform;
    private Vector3 direction;
    private Rigidbody2D rb;
    private Rotation rotation;
    private void Start()
    {
        
        currentTransform = transform;
        direction = currentTransform.right;
        rotation = GetComponent<Rotation>();
        rotation.RotateToDirection(direction);
        rb = GetComponent<Rigidbody2D>();
        bodySegmentGenerator = GetComponent<BodySegmentGenerator>();
        
        bodySegmentGenerator.GenerateBodySegments(3);
        StartCoroutine(RepeatMove());
    }


    public void ChangeDirection(Vector2 newDirection)
    {
        Vector2 temp = GetStraightDirection(newDirection);
        if (direction.x == -temp.x || direction.y == -temp.y)
            return;
        direction.x = temp.x;
        direction.y = temp.y;
        rotation.RotateToDirection(direction);
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

        if(Physics2D.Raycast(rb.position, direction, 1f, ~ignoreMask).collider is null)
        {
            MoveBodySegments();
        }

    }
    private void MoveBodySegments()
    {
        List<Transform> segmentList = bodySegmentGenerator.BodySegments;

        for (int i = segmentList.Count - 1; i > 0; i--)
        {
            segmentList[i].position = segmentList[i - 1].position;
        }
        segmentList[0].position = currentTransform.position;
    }
}
