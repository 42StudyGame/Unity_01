using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public abstract partial class MonoBehaviourPunCustomRelease : IOnEventCallback
{
    public virtual void OnEvent(EventData photonEvent)
    {
        // if (photonEvent.Code.Equals(PhotonCustomEventCode.Release))
        // {
        //     GuestSideRelease(photonEvent);
        // }
    }
    
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    //
    // private void GuestSideRelease(EventData photonEvent)
    // {
    //     if (PhotonNetwork.IsMasterClient)
    //     {
    //         return;
    //     }
    //     
    //     // var data = (object[])photonEvent.CustomData;
    //     // var viewId = (int)data[0];
    //     var viewId = (int)photonEvent.CustomData;
    //
    //     Debug.Log($"call GuestSideRelease {viewId}");        
    //     // ReleaseItemByViewId(viewId);
    //     // 어떻게하지;;;
    // }

}

public abstract partial class MonoBehaviourPunCustomRelease : MonoBehaviourPun
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