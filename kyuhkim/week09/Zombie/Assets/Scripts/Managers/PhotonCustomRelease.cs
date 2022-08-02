using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public abstract class MonoBehaviourPunCustomRelease : MonoBehaviourPun
{
    protected PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public void NetworkRelease()
    {
Debug.Log("call NetworkRelease");
        // var data = new object[]
        // {
        //     photonView.ViewID
        // };
        var data = (object)photonView.ViewID;
        
        var raiseEventOptions = new RaiseEventOptions
        {
            CachingOption = EventCaching.RemoveFromRoomCache,
            Receivers = ReceiverGroup.All
        };
        
        var sendOptions = new SendOptions()
        {
            Reliability = true
        };
        
        PhotonNetwork.RaiseEvent(PhotonCustomEventCode.Release, data, raiseEventOptions, sendOptions);
    }
}