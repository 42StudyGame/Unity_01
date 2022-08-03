using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public partial class AmmoPack : IItem
{
    public void Use(GameObject target)
    {
        var shooter = target.GetComponent<PlayerShooter>();

        if (shooter != null && shooter.Weapon != null)
        {
            shooter.Weapon.RefillableCount += Ammo;
        }
        
        Destroy(gameObject);
    }
}

public partial class AmmoPack : MonoBehaviour
{
    private const int Ammo = 30;
}
