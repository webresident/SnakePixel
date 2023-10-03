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
    private bool isMoving = false;
    private void Start()
    {
        
        currentTransform = transform;
        direction = currentTransform.right;
        rotation = GetComponent<Rotation>();
        rotation.RotateToDirection(direction);
        rb = GetComponent<Rigidbody2D>();
        bodySegmentGenerator = GetComponent<BodySegmentGenerator>();
        
        bodySegmentGenerator.GenerateBodySegments(4);
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
            StartCoroutine(SmoothMove());
            if (Physics2D.Raycast(rb.position, direction, 0.75f, ~ignoreMask).collider is null)
            {
                
                rotation.RotateToDirection(direction);
                MoveBodySegments();
            }
            
            yield return new WaitForSeconds(delayTime);
            
        }
    }
    private IEnumerator SmoothMove()
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector2 targetPosition = rb.position + new Vector2(direction.x, direction.y);
        targetPosition = new Vector2(Mathf.Round(targetPosition.x), Mathf.Round(targetPosition.y));
        while (elapsedTime < delayTime)
        {
            rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, (elapsedTime / delayTime)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rb.MovePosition(targetPosition);
        isMoving = false;
    }
    private void MoveBodySegments()
    {
        List<Transform> segmentList = bodySegmentGenerator.BodySegments;

        if (segmentList.Count < 1)
            return;

        for (int i = segmentList.Count - 1; i > 0; i--)
        {
            StartCoroutine(SmoothMoveBodySegment(segmentList[i], segmentList[i - 1].position));
        }
        StartCoroutine(SmoothMoveBodySegment(segmentList[0], currentTransform.position));
    }
    
    private IEnumerator SmoothMoveBodySegment(Transform segmentTransform, Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        Vector2 startPosition = segmentTransform.position;
        targetPosition = new Vector2(Mathf.Round(targetPosition.x), Mathf.Round(targetPosition.y));
        while (elapsedTime < delayTime)
        {
            segmentTransform.position =  Vector3.Lerp(startPosition, targetPosition, (elapsedTime / delayTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        segmentTransform.position = targetPosition;
    }
}
