using System;
using Photon.Pun;
using UnityEngine;

public partial class Coin : IPooledItem
{
    public void Use(GameObject target)
    {
        if (_gameManager == null)
        {
            return;
        }
        
        _gameManager.AddScore(Score);
        photonView.RPC("ReleaseOnServer", RpcTarget.Others);
        Release?.Invoke();
        // PhotonNetwork.Destroy(gameObject);
    }

    public Action Release { get; set; }
}

public partial class Coin : MonoBehaviourPun
{
    private const int Score = 200;
    private GameManager _gameManager = null;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    [PunRPC]
    private void ReleaseOnServer()
    {
        Release?.Invoke();
    }
}
