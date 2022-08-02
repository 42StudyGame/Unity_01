using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public abstract class MonoBehaviourPunCustomRelease : MonoBehaviourPun
{
    protected void NetworkRelease()
    {
        var data = new object[]
        {
            photonView.ViewID
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
        
        PhotonNetwork.RaiseEvent(PhotonCustomEventCode.Release, data, raiseEventOptions, sendOptions);
    }
}