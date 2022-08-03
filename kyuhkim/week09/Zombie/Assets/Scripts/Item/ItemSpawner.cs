using System.Collections;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.AI;

public partial class ItemSpawner : ISpawnSender
{
    public void RaiseSpawnEvent(params object[] param)
    {
        var raiseEventOptions = new RaiseEventOptions
        {
            CachingOption = EventCaching.AddToRoomCache,
            Receivers = ReceiverGroup.Others
        };
        var sendOptions = new SendOptions
        {
            Reliability = true
        };

        PhotonNetwork.RaiseEvent(CustomEventCode.RequestEvent, param, raiseEventOptions, sendOptions);
    }
    
    public void RaiseDespawnEvent(params object[] param)
    {
        var raiseEventOptions = new RaiseEventOptions
        {
            CachingOption = EventCaching.RemoveFromRoomCache,
            Receivers = ReceiverGroup.Others
        };
        var sendOptions = new SendOptions
        {
            Reliability = true
        };

        PhotonNetwork.RaiseEvent(CustomEventCode.ReleaseEvent, param, raiseEventOptions, sendOptions);
    }
}

public partial class ItemSpawner : IOnEventCallback
{
    public void OnEvent(EventData eventData)
    {
Debug.LogWarning("call OnEvent");
        switch (eventData.Code)
        {
            case CustomEventCode.RequestEvent:
                EventSpawn(eventData);
                break;
            case CustomEventCode.ReleaseEvent:
                EventDespawn(eventData);
                break;
            default:
                throw new System.Exception($"wrong event code = [{eventData.Code}]");
        }
    }
}

public partial class ItemSpawner : ISpawnReceiver
{
    public async void EventSpawn(EventData eventData)
    {
        var data = (object[])eventData.CustomData;

        var position = (Vector3)data[0];
        var poolId = (int)data[1];
        var viewId = (int)data[2];
        
        var item = await _poolArray[poolId].RequestBy(viewId);
        item.transform.position = position;
        
        StartCoroutine(DestoryAfter(item, 5f));
    }

    public void EventDespawn(EventData eventData)
    {
        var data = (object[])eventData.CustomData;
        
        var poolId = (int)data[0];
        var viewId = (int)data[1];

        _poolArray[poolId].Release(viewId);
    }
}

public partial class ItemSpawner : MonoBehaviourPun
{
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
        var item = await _poolArray[poolId].RequestBy();

        RaiseSpawnEvent(new object[] { position, poolId, item.ViewID });
        item.transform.position = position;
        StartCoroutine(DestoryAfter(item, 5f));
    }

    private IEnumerator DestoryAfter(PhotonView item, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (!item.gameObject.activeInHierarchy || !item.TryGetComponent(out IPooledItem pooledItem))
        {
            yield break;
        }
        
        RaiseDespawnEvent(new object[] { ContainerId(item.ViewID), item.ViewID });
        pooledItem.Release();
    }

    private static Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        var randomPos = Random.insideUnitSphere * distance + center;
        NavMesh.SamplePosition(randomPos, out var hit, distance, NavMesh.AllAreas);

        return hit.position;
    }

    private int ContainerId(int viewId)
    {
        for (var id = 0; id < _poolArray.Length; ++id)
        {
            if (_poolArray[id].IsAccounted(viewId))
            {
                return id;
            }
        }

        throw new System.Exception($"wrong ViewID = [{viewId}]");
    }
}
