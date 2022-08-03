using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    private int _count = 5;

    private IGameManager _gameManager;
    
    private const float k_timeBetSpawnMin = 1.25f;
    private const float k_timeBetSpawnMax = 2.25f;
    private float _timeBetSpawn;

    private const float k_yMin = -3.5f;
    private const  float k_yMax = 1.5f;
    private const float k_xPos = 20;

    private GameObject[] _platforms;
    private int _currentIndex = 0;

    private Vector2 _poolPosition = Vector2.down * 25;
    private float _lastSpawnTime;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _lastSpawnTime = 0;
        _timeBetSpawn = 0;
        _platforms = new GameObject[_count];

        for (var i = 0; i < _count; ++i)
        {
            _platforms[i] = Instantiate(platformPrefab, _poolPosition, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (_gameManager.IsGameover)
        {
            return;
        }

        if (!(Time.time >= _lastSpawnTime + _timeBetSpawn))
        {
            return;
        }
        
        _lastSpawnTime = Time.time;
        _timeBetSpawn = Random.Range(k_timeBetSpawnMin, k_timeBetSpawnMax);
        var yPos = Random.Range(k_yMin, k_yMax);
            
        _platforms[_currentIndex].SetActive(false);
        _platforms[_currentIndex].SetActive(true);
            
        _platforms[_currentIndex].transform.position = new Vector2(k_xPos, yPos);
        ++_currentIndex;

        _currentIndex %= _count;
    }
}
