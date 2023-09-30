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
                Instantiate(fieldCellPrefab, currentTransform.position + new Vector3(i + 0.5f, j + 0.5f, 0), currentTransform.rotation, currentTransform);
            }
        }
    }
    private void CreateFieldBorders()
    {
        for (int i = 0; i < fieldSize.X; i++)
        {
            Instantiate(borderCellPrefab, currentTransform.position + new Vector3(i + 0.5f, -0.5f, 0), currentTransform.rotation, currentTransform);
            Instantiate(borderCellPrefab, currentTransform.position + new Vector3(i + 0.5f, FieldSize.Y + 0.5f, 0), currentTransform.rotation, currentTransform);
        }
        for (int i = -1; i < fieldSize.Y + 1; i++)
        {
            Instantiate(borderCellPrefab, currentTransform.position + new Vector3(-0.5f, i + 0.5f, 0), currentTransform.rotation, currentTransform);
            Instantiate(borderCellPrefab, currentTransform.position + new Vector3(FieldSize.X + 0.5f, i + 0.5f, 0), currentTransform.rotation, currentTransform);
        }
    }
}
