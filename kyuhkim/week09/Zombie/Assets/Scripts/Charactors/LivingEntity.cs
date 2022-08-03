using System;
using Photon.Pun;
using UnityEngine;

public partial class LivingEntity : ILiving
{
    [PunRPC]
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Durability -= damage;
            photonView.RPC("ApplyUpdatedHealth", RpcTarget.Others, Durability, IsDead);
            photonView.RPC("OnDamage", RpcTarget.Others, damage, hitPoint, hitNormal);
        }

        if (Durability <= 0 && !IsDead)
        {
            Die();
        }
    }

    [PunRPC]
    public virtual void Repair(float changeAmount)
    {
        if (IsDead)
        {
            return;
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        
        Durability += changeAmount;
        Durability = Mathf.Min(Durability, startingHealth);
        photonView.RPC("ApplyUpdatedHealth", RpcTarget.Others, Durability, IsDead);
        photonView.RPC("Repair", RpcTarget.Others, changeAmount);
    }

    public bool IsDead { get; protected set; }
    public float Durability { get; protected set; }
    public event Action OnDeath;
}

public partial class LivingEntity : MonoBehaviourPun
{
    [PunRPC]
    public void ApplyUpdatedHealth(float health, bool isDead)
    {
        Durability = health;
        IsDead = isDead;
    }
    
    public float startingHealth = 100f;

    protected virtual void OnEnable()
    {
        IsDead = false;
        Durability = startingHealth;
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
        IsDead = true;
    }
}
