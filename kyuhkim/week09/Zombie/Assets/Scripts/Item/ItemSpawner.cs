using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviourPun
{
    // [SerializeField] private GameObject[] _itemArray;
    // public Transform playerTransform;
    private static readonly string[] AddressNameArray = new string[] { "AmmoPack", "Coin", "HealthPack" };
    private SyncObjectPool<PhotonView>[] _poolArray;

    private float _maxDistance = 5f;
    private float _timeBetSpawnMax = 7f;
    private float _timeBetSpawnMin = 2f;
    private float _timeBetSpawn;
    private float _lastSpawnTime;

    private void Awake()
    {
        _poolArray = new SyncObjectPool<PhotonView>[AddressNameArray.Length];

        for (var i = 0; i < AddressNameArray.Length; ++i)
        {
            _poolArray[i] = new SyncObjectPool<PhotonView>(AddressNameArray[i]);
        }
    }
    
    private void Start()
    {
        _timeBetSpawn = Random.Range(_timeBetSpawnMin, _timeBetSpawnMax);
        _lastSpawnTime = 0;
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient
            || Time.time < _lastSpawnTime + _timeBetSpawn)
        {
            return;
        }
        
        _lastSpawnTime = Time.time;
        _timeBetSpawn = Random.Range(_timeBetSpawnMin, _timeBetSpawnMax);
        Spawn();
    }

    private async void Spawn()
    {
        var position = GetRandomPointOnNavMesh(Vector3.zero, _maxDistance) + Vector3.up * .5f;
        var poolId = Random.Range(0, AddressNameArray.Length);
        // var selectItem = _itemArray[Random.Range(0, _itemArray.Length)];
        // var item = PhotonNetwork.Instantiate(selectItem.name, position, Quaternion.identity);
        var item = await _poolArray[poolId].RequestBy();

        StartCoroutine(DestoryAfter(item, 5f));
    }

    private IEnumerator DestoryAfter(PhotonView item, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (item.gameObject.activeInHierarchy && item is IPooledItem pooledItem)
        {
            pooledItem.Release();
        }
        // if (item != null)
        // {
        //     PhotonNetwork.Destroy(item);
        // }
    }

    private static Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        var randomPos = Random.insideUnitSphere * distance + center;
        NavMesh.SamplePosition(randomPos, out var hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}
