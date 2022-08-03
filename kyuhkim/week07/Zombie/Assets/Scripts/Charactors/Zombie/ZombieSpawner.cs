using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    public Zombie prefab = null;
    public ZombieData[] dataArray = null;
    public Transform[] pointArray = null;

    private List<Zombie> _zombieList = null;
    private int _wave = 0;

    private GameManager _gameManager = null;
    private UIManager _uiManager = null;

    private void Awake()
    {
        _zombieList = new List<Zombie>();
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        // if (null != _gameManager && _gameManager.IsGameover)
        if (_gameManager is { IsGameover: true })
        {
            return;
        }

        if (_zombieList.Count <= 0)
        {
            SpawnWave();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        _uiManager.UpdateWaveText(_wave, _zombieList.Count);
    }

    private void SpawnWave()
    {
        ++_wave;

        var spawnCount = Mathf.RoundToInt(_wave * 1.5f);

        for (var i = 0; i < spawnCount; ++i)
        {
            CreateZombie();
        }
    }

    private void CreateZombie()
    {
        var data = dataArray[Random.Range(0, dataArray.Length)];
        var point = pointArray[Random.Range(0, pointArray.Length)];
        var zombie = Instantiate(prefab, point.position, point.rotation);
        
        zombie.Setup(data);
        _zombieList.Add(zombie);

        // zombie.OnDeath += () =>
        // {
        //     _zombieList.Remove(zombie);
        //     Destroy(zombie.gameObject, 10f);
        //     _gameManager.AddScore(100);
        // };
        zombie.OnDeath += () => _zombieList.Remove(zombie);
        zombie.OnDeath += () => Destroy(zombie.gameObject, 10f);
        zombie.OnDeath += () => _gameManager.AddScore(100);
    }
}
