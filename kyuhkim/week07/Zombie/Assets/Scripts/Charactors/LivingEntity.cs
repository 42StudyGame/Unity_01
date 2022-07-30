using System;
using UnityEngine;

public partial class LivingEntity : ILiving
{
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Health -= damage;

        if (Health <= 0 && !IsDead)
        {
            Die();
        }
    }

    public virtual void Restore(float changeAmount)
    {
        if (IsDead)
        {
            return;
        }

        Health += changeAmount;
    }

    public bool IsDead { get; protected set; }
    public float Health { get; protected set; }
    public event Action OnDeath;
}

public partial class LivingEntity : MonoBehaviour
{
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
