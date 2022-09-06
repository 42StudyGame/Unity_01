using System;
using UnityEngine;

// 생명체로서 동작할 게임 오브젝트들을 위한 뼈대를 제공
// 체력, 데미지 받아들이기, 사망 기능, 사망 이벤트를 제공
public class LivingEntity : MonoBehaviour, IDamageable {
    public float startingHealth = 100f; // 시작 체력
    public float health { get; protected set; } // 현재 체력
    public bool dead { get; protected set; } // 사망 상태
    public event Action onDeath; // 사망시 발동할 이벤트

	protected virtual void OnEnable() {
		dead = false;
		health = startingHealth;
	}

	public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
		health -= damage;

		if (health <= 0 && !dead)
			Die();
	}

	public virtual void RestoreHealth(float newHealth) {
		if (dead)
			return;
		else
			health += newHealth;
	}

	public virtual void Die() {
		if (onDeath != null)
			onDeath();
		dead = true;
	}
}