using System;
using Photon.Pun;
using UnityEngine;

public partial class HealthPack : IPooledItem
{
    public void Use(GameObject target)
    {
        if (!target.TryGetComponent(out LivingEntity life))
        {
            return;
        }
        
        life.Repair(Health);
        PhotonNetwork.Destroy(gameObject);
    }

    public Action Release { get; set; }
}

public partial class HealthPack : MonoBehaviourPun
{
    private const int Health = 50;
}
