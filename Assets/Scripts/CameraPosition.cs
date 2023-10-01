using UnityEngine;

public enum CameraOrientation
{
    Horizontal = 1 << 0,
    Vertical = 1 << 1
}
public class CameraPosition : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float borderOffset = 1f;
    [SerializeField] private CameraOrientation cameraOrientation = CameraOrientation.Horizontal;
    private GameField gameField;

    private void Start()
    {
        gameField = targetObject.GetComponent<GameField>();

        switch (cameraOrientation)
        {
            case CameraOrientation.Horizontal:
                InitializeHorizontalOrientation();
                break;
            case CameraOrientation.Vertical:
                InitializeVerticalOrientation();
                break;
        }
    }

    private void InitializeHorizontalOrientation()
    {
        transform.position = new Vector3((float)gameField.FieldSize.X / 2, (float)gameField.FieldSize.Y / 2, transform.position.z);
        GetComponent<Camera>().orthographicSize = gameField.FieldSize.Y / 2 + borderOffset;
    }

    private void InitializeVerticalOrientation()
    {
        transform.position = new Vector3((float)gameField.FieldSize.X / 2, (float)gameField.FieldSize.Y / 2 - 1.5f, transform.position.z);
        GetComponent<Camera>().orthographicSize = gameField.FieldSize.Y / 2 + borderOffset + 5f;
    }
}
