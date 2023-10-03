using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{
    [SerializeField] private GameField gameField;
    [SerializeField] private GameObject snakePrefab;
    //[SerializeField] private MovementInput movementInput;
    [SerializeField] private SwipeInput swipeInput;

    private List<GameObject> snakeChildren = new List<GameObject>();

    private void Start()
    {
        Spawn();

        foreach (var eachChild in snakeChildren)
        {
            //movementInput.OnButtonClicked += eachChild.GetComponent<Movement>().ChangeDirection;
            swipeInput.OnSwipeEnd += eachChild.GetComponent<Movement>().ChangeDirection;
        }
    }

    private void Spawn()
    {
        GameObject snake = Instantiate(snakePrefab, transform.position + new Vector3(gameField.FieldSize.X / 2, gameField.FieldSize.Y / 2, 0f), transform.rotation, transform);
        snakeChildren.Add(snake);
    }

    private void OnDestroy()
    {
        foreach (var eachChild in snakeChildren)
        {
            //movementInput.OnButtonClicked -= eachChild.GetComponent<Movement>().ChangeDirection;
            swipeInput.OnSwipeEnd -= eachChild.GetComponent<Movement>().ChangeDirection;
        }
    }
}
