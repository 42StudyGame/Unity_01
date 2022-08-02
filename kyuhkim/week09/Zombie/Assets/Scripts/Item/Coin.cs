using UnityEngine;

public partial class Coin : IPhotonPoolItem
{
    public void Use(GameObject target)
    {
        if (_gameManager == null)
        {
            return;
        }
        
        _gameManager.AddScore(Score);
        // make release event;
        //PhotonNetwork.Destroy(gameObject);
        NetworkRelease();
        Release();
    }

    public IObjectPool Home { get; set; }
    
    public int Viewid
    {
        get => _photonView.ViewID;
        set => _photonView.ViewID = value;
    }

    public void Release()
    {
        Home.Release(gameObject);
    }
}

public partial class Coin : MonoBehaviourPunCustomRelease
{
    private const int Score = 200;
    private GameManager _gameManager = null;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
}
