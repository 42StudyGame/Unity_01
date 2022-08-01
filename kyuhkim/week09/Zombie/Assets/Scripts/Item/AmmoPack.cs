using Photon.Pun;
using UnityEngine;

public partial class AmmoPack : IItem
{
    public void Use(GameObject target)
    {
        var shooter = target.GetComponent<PlayerShooter>();

        if (shooter != null && shooter.Weapon is Gun gun)
        {
            gun.photonView.RPC("AddAmmo", RpcTarget.All, Ammo);
        }

        PhotonNetwork.Destroy(gameObject);
    }
}

public partial class AmmoPack : MonoBehaviourPun
{
    private const int Ammo = 30;
}
