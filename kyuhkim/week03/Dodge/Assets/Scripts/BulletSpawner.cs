using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject bulletPrefab;
    private const float SpawnRateMin = .5f;
    private const float SpawnRateMax = 3f;

    private Transform _target;
    private float _spawnRate;
    private float _timeAfterSpawn;
    
    private void Start()
    {
        _target = FindObjectOfType<PlayerController>().transform;
        SetSpawnRate();
    }

    private void Update()
    {
        _timeAfterSpawn += Time.deltaTime;

        if (_timeAfterSpawn < _spawnRate)
        {
            return;
        }

        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.LookAt(_target);
        SetSpawnRate();
    }

    private void SetSpawnRate()
    {
        _timeAfterSpawn = 0;
        _spawnRate = Random.Range(SpawnRateMin, SpawnRateMax);   
    }
}
