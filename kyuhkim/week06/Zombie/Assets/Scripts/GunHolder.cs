using UnityEngine;

public partial class GunHolder : IWeaponHolder
{
    public IWeapon Weapon
    {
        get => currentWeapon.GetComponent<IWeapon>();
    }
}

public partial class GunHolder : MonoBehaviour
{
    public GameObject currentWeapon;
}
