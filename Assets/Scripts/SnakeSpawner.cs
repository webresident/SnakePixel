using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{
    [SerializeField] private GameField gameField;
    [SerializeField] private GameObject snakePrefab;
    [SerializeField] private MovementInput movementInput;

    private List<GameObject> snakeChildren = new List<GameObject>();

    private void Start()
    {
        Spawn();

        foreach (var eachChild in snakeChildren)
        {
            movementInput.OnButtonClicked += eachChild.GetComponent<Movement>().ChangeDirection;
        }
    }

    private void Spawn()
    {
        GameObject snake = Instantiate(snakePrefab, transform.position + new Vector3(gameField.FieldSize.X / 2 + 0.5f, gameField.FieldSize.Y / 2 + 0.5f, 0f), transform.rotation, transform);
        snakeChildren.Add(snake);
    }

    private void OnDestroy()
    {
        foreach (var eachChild in snakeChildren)
        {
            movementInput.OnButtonClicked -= eachChild.GetComponent<Movement>().ChangeDirection;
        }
    }
}
