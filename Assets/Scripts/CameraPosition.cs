using UnityEngine;

public enum CameraOrientation
{
    Horizontal = 1 << 0,
    Vertical = 1 << 1
}
public class CameraPosition : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    private float horizontalBorderOffset = 1f;
    private float verticalBorderOffset = 6f;
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
        GetComponent<Camera>().orthographicSize = gameField.FieldSize.Y / 2 + horizontalBorderOffset;
    }

    private void InitializeVerticalOrientation()
    {


        float fitSize = ((float)gameField.FieldSize.X / gameField.FieldSize.Y) / ((float)Screen.width / Screen.height) * (gameField.FieldSize.X + 4f);
        fitSize = Mathf.Round(fitSize);
        GetComponent<Camera>().orthographicSize = fitSize;

        float yOffset = (fitSize - (gameField.FieldSize.X + 4f)) + 1.5f;
        transform.position = new Vector3((float)gameField.FieldSize.X / 2, (float)gameField.FieldSize.Y / 2 - yOffset, transform.position.z);
    }
}
