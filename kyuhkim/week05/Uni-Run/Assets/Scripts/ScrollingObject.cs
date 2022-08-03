using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private float _speed = 5f;
    private IGameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (!_gameManager.IsGameover)
        {
            transform.Translate(Vector3.left * (_speed * Time.deltaTime));
        }
    }
}
