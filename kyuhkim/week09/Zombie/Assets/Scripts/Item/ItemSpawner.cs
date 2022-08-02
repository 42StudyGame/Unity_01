using System.Threading.Tasks;
using System.Collections;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.AI;

public partial class ItemSpawner : MonoBehaviourPun
{
    private IObjectPool[] _itemPool;
    [SerializeField] private GameObject[] itemArray;
    
    private const float MaxDistance = 5f;
    private const float TimeBetSpawnMax = 7f;
    private const float TimeBetSpawnMin = 2f;
    private float _timeBetSpawn;
    private float _lastSpawnTime;

    // private const byte CustomManualRequestEventCode = 1;
    // private const byte CustomManualReleaseEventCode = 2;

    private void Start()
    {
        _timeBetSpawn = Random.Range(TimeBetSpawnMin, TimeBetSpawnMax);
        _lastSpawnTime = 0;
        _itemPool = new IObjectPool[3];
        
        _itemPool[0] = new ObjectPool();
        _itemPool[1] = new ObjectPool();
        _itemPool[2] = new ObjectPool();

        _itemPool[0].SetPrefab("modelingData_ammo");
        _itemPool[1].SetPrefab("modelingData_coin");
        _itemPool[2].SetPrefab("modelingData_healthPack");
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
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache
        };

        var sendOptions = new SendOptions()
        {
            Reliability = true
        };

        PhotonNetwork.RaiseEvent(PhotonCustomEventCode.Request, data, raiseEventOptions, sendOptions);
        go.transform.position = position;
        StartCoroutine(ReleaseAfter(go, 5f));
    }

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

    private void GuestSideRelease(EventData photonEvent)
    {
        
    }
    
    private async Task GuestSideRequest(EventData photonEvent)
    {
        var data = (object[]) photonEvent.CustomData;
        var go = await _itemPool[(int)data[2]].Request();
        var item = go.GetComponent<PhotonView>();
        go.transform.position = (Vector3)data[0];

        item.ViewID = (int)data[1];
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
