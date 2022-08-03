using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemArray;
    public Transform playerTransform;
    
    private float _maxDistance = 5f;
    private float _timeBetSpawnMax = 7f;
    private float _timeBetSpawnMin = 2f;
    private float _timeBetSpawn;
    private float _lastSpawnTime;

    private void Start()
    {
        _timeBetSpawn = Random.Range(_timeBetSpawnMin, _timeBetSpawnMax);
        _lastSpawnTime = 0;
    }

    private void Update()
    {
        if (!(Time.time >= _lastSpawnTime + _timeBetSpawn) || playerTransform == null)
        {
            return;
        }
        _lastSpawnTime = Time.time;
        _timeBetSpawn = Random.Range(_timeBetSpawnMin, _timeBetSpawnMax);
        Spawn();
    }

    private void Spawn()
    {
        var position = GetRandomPointOnNavMesh(playerTransform.position, _maxDistance) + Vector3.up * .5f;

        var selectItem = _itemArray[Random.Range(0, _itemArray.Length)];
        var item = Instantiate(selectItem, position, Quaternion.identity);
        
        Destroy(item, 5f);
    }

    private static Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        var randomPos = Random.insideUnitSphere * distance + center;
        NavMesh.SamplePosition(randomPos, out var hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}
