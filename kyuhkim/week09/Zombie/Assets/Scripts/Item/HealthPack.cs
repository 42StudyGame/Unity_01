using UnityEngine;

public partial class HealthPack : IPhotonPoolItem
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
}

public partial class HealthPack : MonoBehaviourPunCustomRelease
{
    private const int Health = 50;
}
