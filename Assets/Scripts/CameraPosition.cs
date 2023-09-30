using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float borderOffset = 1f;
    private GameField gameField;

    private void Start()
    {
        gameField = targetObject.GetComponent<GameField>();

        transform.position = new Vector3((float)gameField.FieldSize.X / 2, (float)gameField.FieldSize.Y / 2, transform.position.z);
        GetComponent<Camera>().orthographicSize = gameField.FieldSize.Y / 2 + borderOffset;
    }
}
