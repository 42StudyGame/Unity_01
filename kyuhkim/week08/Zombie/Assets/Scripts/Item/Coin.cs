using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Photon.Pun;
using UnityEngine;

public partial class Coin : IItem
{
    public void Use(GameObject target)
    {
        if (_gameManager == null)
        {
            return;
        }
        
        _gameManager.AddScore(Score);
        PhotonNetwork.Destroy(gameObject);
    }
}

public partial class Coin : MonoBehaviour
{
    private const int Score = 200;
    private GameManager _gameManager = null;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
}
