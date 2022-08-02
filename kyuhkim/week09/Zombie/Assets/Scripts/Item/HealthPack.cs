using Photon.Pun;
using UnityEngine;

public partial class HealthPack : IPoolItem
{
    public void Use(GameObject target)
    {
        if (!target.TryGetComponent(out LivingEntity life))
        {
            return;
        }
        
        life.Restore(Health);
        // make release event;
        // PhotonNetwork.Destroy(gameObject);
        NetworkRelease();
        Release();
    }

    public IObjectPool Home { get; set; }
    
    public void Release()
    {
        Home.Release(gameObject);
    }
}

public partial class HealthPack : MonoBehaviourPunCustomRelease
{
    private const int Health = 50;
}
