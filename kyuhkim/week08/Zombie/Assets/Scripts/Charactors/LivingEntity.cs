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
            Health -= damage;
            photonView.RPC("ApplyUpdatedHealth", RpcTarget.Others, Health, IsDead);
            photonView.RPC("OnDamage", RpcTarget.Others, damage, hitPoint, hitNormal);
        }

        if (Health <= 0 && !IsDead)
        {
            Die();
        }
    }

    [PunRPC]
    public virtual void Restore(float changeAmount)
    {
        if (IsDead)
        {
            return;
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        
        Health += changeAmount;
        photonView.RPC("ApplyUpdatedHealth", RpcTarget.Others, Health, IsDead);
        photonView.RPC("Restore", RpcTarget.Others, changeAmount);
    }

    public bool IsDead { get; protected set; }
    public float Health { get; protected set; }
    public event Action OnDeath;
}

public partial class LivingEntity : MonoBehaviourPun
{
    [PunRPC]
    public void ApplyUpdatedHealth(float health, bool isDead)
    {
        Health = health;
        IsDead = isDead;
    }
    
    public float startingHealth = 100f;

    protected virtual void OnEnable()
    {
        IsDead = false;
        Health = startingHealth;
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
        IsDead = true;
    }
}
