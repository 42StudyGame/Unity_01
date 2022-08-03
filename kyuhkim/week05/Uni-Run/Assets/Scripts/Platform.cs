using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles;
    private IGameManager _gameManager;
    private bool _stepped = false;
    private const int k_obstacleRatio = 3;
    private const string k_player = "Player";
    private int _score;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        _stepped = false;
        _score = 1;
        
        foreach (var item in obstacles)
        {
            var obstacleActivate = Random.Range(0, k_obstacleRatio).Equals(0); 
            item.SetActive(obstacleActivate);
            if (obstacleActivate)
            {
                ++_score;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag(k_player) || _stepped)
        {
            return;
        }
        
        _stepped = true;
        _gameManager.AddScore(_score);
    }
}
