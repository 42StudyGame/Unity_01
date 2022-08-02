using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public abstract class MonoBehaviourPunCustomRelease : MonoBehaviourPun
{
    protected PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    protected void NetworkRelease()
    {
        // var data = new object[]
        // {
        //     photonView.ViewID
        // };
        var data = (object)photonView.ViewID;
        
        var raiseEventOptions = new RaiseEventOptions
        {
            CachingOption = EventCaching.AddToRoomCache,
            Receivers = ReceiverGroup.All
        };
        
        var sendOptions = new SendOptions()
        {
            Reliability = true
        };
        
        PhotonNetwork.RaiseEvent(PhotonCustomEventCode.Release, data, raiseEventOptions, sendOptions);
    }
}