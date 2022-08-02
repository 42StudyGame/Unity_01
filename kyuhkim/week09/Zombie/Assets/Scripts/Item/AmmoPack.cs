using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public partial class AmmoPack : IPoolItem
{
    private Action _release;

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
    
    public IObjectPool Home { get; set; }

    public void Release()
    {
        Home.Release(gameObject);
    }
}

public partial class AmmoPack : MonoBehaviourPunCustomRelease
{
    private const int Ammo = 30;

}
