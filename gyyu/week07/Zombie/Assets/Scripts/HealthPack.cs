using UnityEngine;

// 체력을 회복하는 아이템
public class HealthPack : MonoBehaviour, IItem {
	public float health = 50;

	public void Use(GameObject target) {
		LivingEntity lift = GetComponent<LivingEntity>();
		
		if (lift != null)
			lift.RestoreHealth(health);
		Destroy(gameObject);
	}
}