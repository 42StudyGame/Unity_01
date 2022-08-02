using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public partial class Coin : IPhotonPoolItem
{
    public void Use(GameObject target)
    {
        if (_gameManager == null)
        {
            return;
        }
        
        _gameManager.AddScore(Score);
        NetworkRelease();
        Release();
    }

    public IPhotonObjectPool Home { get; set; }
    
    public int Viewid
    {
        get => _photonView.ViewID;
        set => _photonView.ViewID = value;
    }

    public void Release()
    {
        NetworkRelease();
        Home.Release(Viewid);
    }

    public override void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code.Equals(PhotonCustomEventCode.Release))
        {
            Release();
        }
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
