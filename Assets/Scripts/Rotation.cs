using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class Rotation : MonoBehaviour
{
    private Transform currentTransform;
    private void Awake()
    {
        currentTransform = transform;
    }

    public void RotateToDirection(Vector2 direction)
    {
        currentTransform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

}
