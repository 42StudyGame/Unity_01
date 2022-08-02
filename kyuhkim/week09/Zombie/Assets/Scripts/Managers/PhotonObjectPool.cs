using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AddressableAssets;

public partial class PhotonObjectPool : IPhotonObjectPool
{
    public async Task SetPrefab(string path)
    {
        if (_prefab != null)
        {
            return;
        }
    
        var operationHandle = Addressables.LoadAssetAsync<GameObject>(path);
    
        while (!operationHandle.IsDone)
        {
            await Task.Delay(30);
        }

        try
        {
            _prefab = operationHandle.Result.GetComponent<PhotonView>();
        }
        catch
        {
            throw new Exception($"Load fail\nkey = [{path}]");
        }
        
        FillOne();
    }
    
    public async Task<PhotonView> Request()
    {
        while (_prefab == null)
        {
            Debug.Log("wait loading...");
            await Task.Delay(30);
        }
Debug.LogWarning($"remain count = {_queue.Count}");

        var value = _queue.Dequeue();
        value.gameObject.SetActive(true);
        if (_queue.Count.Equals(0))
        {
            FillOne();
        }

        _account.Add(value.GetComponent<IPhotonPoolItem>().Viewid, value);
        return value;
    }

    public void Release(int key)
    {
        var value = _account[key];
        _account.Remove(key);
        _queue.Enqueue(value);
        value.gameObject.SetActive(false);
Debug.Log($"Released count = {_queue.Count}");        
    }

    public bool Search(int id)
    {
        return _account.ContainsKey(id);
    }
}

public partial class PhotonObjectPool
{
    private PhotonView _prefab = null;
    private Queue<PhotonView> _queue;
    private Dictionary<int, PhotonView> _account;

    public PhotonObjectPool()
    {
        _queue = new Queue<PhotonView>();
        _account = new Dictionary<int, PhotonView>();
    }

    private void FillOne()
    {
        var value = UnityEngine.Object.Instantiate(_prefab);

        value.GetComponent<IPhotonPoolItem>().Home = this;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AllocateViewID(value);
        }
        
        _queue.Enqueue(value);
        value.gameObject.SetActive(false);
    }
}
