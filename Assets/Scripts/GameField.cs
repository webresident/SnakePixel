using UnityEngine;

[System.Serializable]
public struct FieldSize
{
    public int X;
    public int Y;
    public FieldSize(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class GameField : MonoBehaviour
{
    [SerializeField] private FieldSize fieldSize = new FieldSize(21, 12);
    [SerializeField] private GameObject fieldCellPrefab;
    [SerializeField] private GameObject borderCellPrefab;
    [SerializeField] private GameObject cornerCellPrefab;

    private Transform currentTransform;

    public FieldSize FieldSize { get { return fieldSize; } }

    private void Start()
    {
        currentTransform = transform;
        CreateField();
        CreateFieldBorders();
    }

    private void CreateField()
    {
        for(int i = 0; i < fieldSize.X; i++)
        {
            for(int j = 0; j < fieldSize.Y; j++)
            {
                Instantiate(fieldCellPrefab, currentTransform.position + new Vector3(i, j, 0), currentTransform.rotation, currentTransform);
            }
        }
    }
    private void CreateFieldBorders()
    {
        Quaternion turnoverRotation = Quaternion.Euler(0, 0, 180);

        Quaternion verticalRotation = currentTransform.rotation * Quaternion.Euler(0, 0, 90);
        for (int i = 0; i < fieldSize.X; i++)
        {
            Instantiate(borderCellPrefab, currentTransform.position + new Vector3(i, -1f, 0), verticalRotation, currentTransform);
            Instantiate(borderCellPrefab, currentTransform.position + new Vector3(i, FieldSize.Y, 0), verticalRotation * turnoverRotation, currentTransform);
        }

        for (int i = 0; i < fieldSize.Y; i++)
        {
            Instantiate(borderCellPrefab, currentTransform.position + new Vector3(-1f, i, 0), currentTransform.rotation, currentTransform);
            Instantiate(borderCellPrefab, currentTransform.position + new Vector3(FieldSize.X, i, 0), currentTransform.rotation * turnoverRotation, currentTransform);
        }

        Instantiate(cornerCellPrefab, currentTransform.position + new Vector3(-1f,  -1f, 0), currentTransform.rotation * verticalRotation, currentTransform);
        Instantiate(cornerCellPrefab, currentTransform.position + new Vector3(-1f, fieldSize.Y, 0), currentTransform.rotation, currentTransform);
        var temp = Instantiate(cornerCellPrefab, currentTransform.position + new Vector3(fieldSize.X, fieldSize.Y, 0), currentTransform.rotation, currentTransform).GetComponent<Transform>();
        temp.localScale = new Vector3(-temp.localScale.x, temp.localScale.y, temp.localScale.z);
        temp = Instantiate(cornerCellPrefab, currentTransform.position + new Vector3(fieldSize.X, -1f, 0), currentTransform.rotation * verticalRotation, currentTransform).GetComponent<Transform>();
        temp.localScale = new Vector3(temp.localScale.x, -temp.localScale.y, temp.localScale.z);
    }
}
