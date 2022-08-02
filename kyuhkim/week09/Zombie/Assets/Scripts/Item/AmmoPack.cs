using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public partial class AmmoPack : IPhotonPoolItem
{
    public void Use(GameObject target)
    {
        if (target == null || !target.TryGetComponent(out PlayerShooter shooter) ||
            shooter.Weapon is not Gun gun)
        {
            return;
        }
        
        gun.photonView.RPC("AddAmmo", RpcTarget.All, Ammo);

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
        // Home.Release(gameObject);
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

public partial class AmmoPack : MonoBehaviourPunCustomRelease
{
    private const int Ammo = 30;
}
