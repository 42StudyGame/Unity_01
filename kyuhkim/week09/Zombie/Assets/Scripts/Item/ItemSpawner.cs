using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public partial class ItemSpawner : IOnEventCallback
{
    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case PhotonCustomEventCode.Release:
                GuestSideRelease(photonEvent);
                break;
            case PhotonCustomEventCode.Request:
                GuestSideRequest(photonEvent);
                break;
        }
    }

}

public partial class ItemSpawner : MonoBehaviourPun
{
    private static readonly string[] _dataName = new string[3] { "modelingData_ammo", "modelingData_ammo", "modelingData_ammo" };
    private IObjectPool[] _itemPool;
    [SerializeField] private GameObject[] itemArray;
    
    private const float MaxDistance = 5f;
    private const float TimeBetSpawnMax = 7f;
    private const float TimeBetSpawnMin = 2f;
    private float _timeBetSpawn;
    private float _lastSpawnTime;

    private void Start()
    {
        _timeBetSpawn = Random.Range(TimeBetSpawnMin, TimeBetSpawnMax);
        _lastSpawnTime = 0;
        _itemPool = new IObjectPool[_dataName.Length];

        _itemPool = _itemPool.Select((item, index) => {
            item = new ObjectPool();
            item.SetPrefab(_dataName[index]);
            return item;
        }).ToArray();
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient
            || Time.time < _lastSpawnTime + _timeBetSpawn)
        {
            return;
        }
        
        _lastSpawnTime = Time.time;
        _timeBetSpawn = Random.Range(TimeBetSpawnMin, TimeBetSpawnMax);
        Spawn();
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private async void Spawn()
    {
        var position = GetRandomPointOnNavMesh(Vector3.zero, MaxDistance) + Vector3.up * .5f;
        var poolId = Random.Range(0, _itemPool.Length);
        var go = await _itemPool[poolId].Request();
        var item = go.GetComponent<PhotonView>();
        
        var data = new object[]
        {
            position,
            item.ViewID,
            poolId
        };

        var raiseEventOptions = new RaiseEventOptions
        {
            CachingOption = EventCaching.AddToRoomCache,
            Receivers = ReceiverGroup.All
        };

        var sendOptions = new SendOptions()
        {
            Reliability = true
        };

        PhotonNetwork.RaiseEvent(PhotonCustomEventCode.Request, data, raiseEventOptions, sendOptions);
        go.transform.position = position;
        StartCoroutine(ReleaseAfter(go, 5f));
    }

    private void GuestSideRelease(EventData photonEvent)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return;
        }
        
        var data = (object[])photonEvent.CustomData;
        var viewId = (int)data[0];
        // var viewId = (int)photonEvent.CustomData;

        foreach (var container in _itemPool)
        {
            if (!container.Search(viewId))
            {
                continue;
            }
            
            container.Release(viewId);
            break;
        }
    }
    
    private async Task GuestSideRequest(EventData photonEvent)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return;
        }
        var data = (object[]) photonEvent.CustomData;
        var go = await _itemPool[(int)data[2]].Request();

        go.GetComponent<IPhotonPoolItem>().Viewid = (int)data[1];
        go.transform.position = (Vector3)data[0];
        
        StartCoroutine(ReleaseAfter(go, 5f));
    }
    
    private static IEnumerator ReleaseAfter(GameObject item, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        
        if (item != null)
        {
            item.GetComponent<IPoolItem>().Release();
        }
    }

    private static Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        var randomPos = Random.insideUnitSphere * distance + center;
        NavMesh.SamplePosition(randomPos, out var hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}
