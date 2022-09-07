using UnityEngine;

// 총알을 충전하는 아이템
public class AmmoPack : MonoBehaviour, IItem {
    public int ammo = 30;

    public void Use(GameObject target) {
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        if (playerShooter != null && playerShooter.gun != null)
            playerShooter.gun.ammoRemain += ammo;

        Destroy(gameObject);
    }
}