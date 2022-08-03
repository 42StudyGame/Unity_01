using Photon.Pun;
using UnityEngine;

public partial class HealthPack : IItem
{
    public void Use(GameObject target)
    {
        if (!target.TryGetComponent(out LivingEntity life))
        {
            return;
        }
        
        life.Restore(Health);
        PhotonNetwork.Destroy(gameObject);
    }
}

public partial class HealthPack : MonoBehaviourPun
{
    private const int Health = 50;
}
