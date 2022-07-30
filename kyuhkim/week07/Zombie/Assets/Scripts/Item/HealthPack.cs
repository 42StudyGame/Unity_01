using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HealthPack : IItem
{
    public void Use(GameObject target)
    {
        var life = target.GetComponent<LivingEntity>();

        if (life == null)
        {
            return;
        }
        
        life.Restore(Health);
        Destroy(gameObject);
    }
}

public partial class HealthPack : MonoBehaviour
{
    private const int Health = 50;
}
