using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public partial class ObjectPool : IObjectPool
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
    
        _prefab = operationHandle.Result;
    
        if (_prefab == null)
        {
            throw new Exception($"Load fail\nkey = [{path}]");
        }
        
        FillOne();
    }
    
    public async Task<GameObject> Request()
    {
        while (_prefab == null)
        {
            Debug.Log("wait loading...");
            await Task.Delay(30);
        }

        var value = _queue.Dequeue();
        value.SetActive(true);

        if (_queue.Count.Equals(0))
        {
            FillOne();
        }

        _account.Add(value.GetComponent<IPhotonPoolItem>().Viewid, value);
        return value;
    }

    public void Release(GameObject target)
    {
        var key = target.GetComponent<IPhotonPoolItem>().Viewid;

        _account.Remove(key);
        _queue.Enqueue(target);
        target.SetActive(false);
    }

    public void Release(int key)
    {
        var value = _account[key];
        _account.Remove(key);
        _queue.Enqueue(value);
        value.SetActive(false);
    }

    public bool Search(int id)
    {
        return _account.ContainsKey(id);
    }
}

public partial class ObjectPool
{
    private GameObject _prefab = null;
    private Queue<GameObject> _queue;
    private Dictionary<int, GameObject> _account;

    public ObjectPool()
    {
        _queue = new Queue<GameObject>();
        _account = new Dictionary<int, GameObject>();
    }

    private void FillOne()
    {
        var value = UnityEngine.Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        value.GetComponent<IPhotonPoolItem>().Home = this;
        
        _queue.Enqueue(value);
        value.SetActive(false);
    }
}
