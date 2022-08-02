using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public partial class ZombieSpawner : IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_zombieCount);
            stream.SendNext(_wave);
        }
        else
        {
            _zombieCount = (int)stream.ReceiveNext();
            _wave = (int)stream.ReceiveNext();
        }
    }
}

public partial class ZombieSpawner : MonoBehaviourPunCallbacks
{
    public Zombie prefab = null;
    public ZombieData[] dataArray = null;
    public Transform[] pointArray = null;

    private List<Zombie> _zombieList = null;
    private int _zombieCount = 0;
    private int _wave = 0;

    private GameManager _gameManager = null;
    private UIManager _uiManager = null;

    private void Awake()
    {
        PhotonPeer.RegisterType(typeof(Color), 128, ColorSerialization.SerializeColor,
            ColorSerialization.DeserializeColor);
        _zombieList = new List<Zombie>();
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (_gameManager is { IsGameover: true })
            {
                return;
            }

            if (_zombieList.Count.Equals(0))
            {
                // SpawnWave();
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _uiManager.UpdateWaveText(_wave, _zombieList.Count);
        }
        else
        {
            _uiManager.UpdateWaveText(_wave, _zombieCount);
        }
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
        var goZombie = PhotonNetwork.Instantiate(prefab.gameObject.name, point.position, point.rotation);
        var zombie = goZombie.GetComponent<Zombie>();

        zombie.photonView.RPC("Setup", RpcTarget.All, data.health, data.damage, data.speed, data.skilColor);
        _zombieList.Add(zombie);
        
        zombie.OnDeath += () => _zombieList.Remove(zombie);
        zombie.OnDeath += () => StartCoroutine(DestroyAfter(goZombie, 10f));
        zombie.OnDeath += () => _gameManager.AddScore(100);
    }

    private static IEnumerator DestroyAfter(GameObject target, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (target != null)
        {
            PhotonNetwork.Destroy(target);
        }
    }
}
