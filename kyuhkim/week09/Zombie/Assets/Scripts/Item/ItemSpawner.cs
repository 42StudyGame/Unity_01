using System.Threading.Tasks;
using System.Collections;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.AI;

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
    
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void RaiseEvent(Vector3 position, int viewId, int poolId)
    {
        var data = new object[]
        {
            position,
            viewId,
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
    }
}

public partial class ItemSpawner : MonoBehaviourPun
{
    private static readonly string[] DataName = new string[3] { "modelingData_ammo", "modelingData_ammo", "modelingData_ammo" };
    // private static readonly string[] DataName = new string[3] { "modelingData_ammo", "modelingData_coin", "modelingData_healthPack" };
    private IPhotonObjectPool[] _photonItemPool = null;
    
    private const float MaxDistance = 5f;
    private const float TimeBetSpawnMax = 7f;
    private const float TimeBetSpawnMin = 2f;
    private float _timeBetSpawn;
    private float _lastSpawnTime;
    private bool _readyToGo = false;

    private async void Start()
    {
        _timeBetSpawn = Random.Range(TimeBetSpawnMin, TimeBetSpawnMax);
        _lastSpawnTime = 0;
        _photonItemPool = new IPhotonObjectPool[DataName.Length];

        for (var i = 0; i < DataName.Length; ++i)
        {
            _photonItemPool[i] = new PhotonObjectPool();
            await _photonItemPool[i].SetPrefab(DataName[i]);
        }

        _readyToGo = true;
    }

    private void Update()
    {
        if (!_readyToGo
            || !PhotonNetwork.IsMasterClient
            || Time.time < _lastSpawnTime + _timeBetSpawn)
        {
            return;
        }
        
        _lastSpawnTime = Time.time;
        _timeBetSpawn = Random.Range(TimeBetSpawnMin, TimeBetSpawnMax);
        Spawn();
    }

    private async void Spawn()
    {
        var position = GetRandomPointOnNavMesh(Vector3.zero, MaxDistance) + Vector3.up * .5f;
        var poolId = Random.Range(0, _photonItemPool.Length);
        var go = await _photonItemPool[poolId].Request();
        var viewId = go.GetComponent<PhotonView>().ViewID;
        
        RaiseEvent(position, viewId, poolId);
        
        go.transform.position = position;
        StartCoroutine(ReleaseAfter(viewId, 5f));
    }

    private void GuestSideRelease(EventData photonEvent)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return;
        }
        
        // var data = (object[])photonEvent.CustomData;
        // var viewId = (int)data[0];
        var viewId = (int)photonEvent.CustomData;

Debug.Log($"call GuestSideRelease {viewId}");        
        ReleaseItemByViewId(viewId);
    }
    
    private async Task GuestSideRequest(EventData photonEvent)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return;
        }
        
        var data = (object[]) photonEvent.CustomData;
        var position = (Vector3)data[0];
        var viewId = (int)data[1];
        var poolId = (int)data[2];
        
        var go = await _photonItemPool[poolId].Request();

        go.GetComponent<IPhotonPoolItem>().Viewid = viewId;
        go.transform.position = position;
        
        StartCoroutine(ReleaseAfter(viewId, 5f));
    }
    
    private IEnumerator ReleaseAfter(int viewId, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        ReleaseItemByViewId(viewId);
    }

    private void ReleaseItemByViewId(int viewId)
    {
        foreach (var container in _photonItemPool)
        {
            if (!container.Search(viewId))
            {
                continue;
            }
            
            container.Release(viewId);
            break;
        }
    }
    
    private static Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        NavMesh.SamplePosition(
            Random.insideUnitSphere * distance + center, 
            out var hit, 
            distance, 
            NavMesh.AllAreas);

        return hit.position;
    }
}
