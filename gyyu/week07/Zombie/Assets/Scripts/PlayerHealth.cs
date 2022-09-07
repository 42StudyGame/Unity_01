using System;
using UnityEngine;
using UnityEngine.UI; // UI 관련 코드

// 플레이어 캐릭터의 생명체로서의 동작을 담당
public class PlayerHealth : LivingEntity {	// LivingEntity를 상속받는다.
    public Slider healthSlider;				// 체력을 표시할 UI 슬라이더
    public Slider screenHSlider;			// 체력을 표시할 UI 슬라이더
    public AudioClip deathClip;				// 사망 소리
    public AudioClip hitClip;				// 피격 소리
    public AudioClip itemPickupClip;		// 아이템 습득 소리
    
	private AudioSource playerAudioPlayer;	// 플레이어 소리 재생기
    private Animator playerAnimator;		// 플레이어의 애니메이터

	private PlayerMovement playerMovement;	// 플레이어 움직임 컴포넌트
    private PlayerShooter playerShooter;	// 플레이어 슈터 컴포넌트

	private void Awake() {
		playerAnimator = GetComponent<Animator>();
		playerAudioPlayer = GetComponent<AudioSource>();

		playerMovement = GetComponent<PlayerMovement>();
		playerShooter = GetComponent<PlayerShooter>();
	}

	protected override void OnEnable() {
		base.OnEnable();
		
		healthSlider.gameObject.SetActive(true); // 체력 슬라이더 활성화
		healthSlider.maxValue = startingHealth;
		healthSlider.value = health;
		screenHSlider.gameObject.SetActive(true); // 체력 슬라이더 활성화
		screenHSlider.maxValue = startingHealth;
		screenHSlider.value = health;
		playerMovement.enabled = true;
		playerShooter.enabled = true;
	}

	public override void RestoreHealth(float newHealth) {
		base.RestoreHealth(newHealth);
		healthSlider.value = health;
		screenHSlider.value = health;
	}

	public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
		if (!dead)
			playerAudioPlayer.PlayOneShot(hitClip);
		base.OnDamage(damage, hitPoint, hitNormal);
		healthSlider.value = health;
		screenHSlider.value = health;
	}

	public override void Die() {
		base.Die();
		healthSlider.gameObject.SetActive(false);
		screenHSlider.gameObject.SetActive(false);
		playerAnimator.SetTrigger("Die");
		playerAudioPlayer.PlayOneShot(deathClip);
		Cursor.lockState = CursorLockMode.Confined;
		playerMovement.enabled = false;
		playerShooter.enabled = false;
	}

	private void OnTriggerEnter(Collider other) {
		if (!dead)
		{
			IItem item = other.GetComponent<IItem>();
			if (item != null)
			{
				item.Use(gameObject);
				playerAudioPlayer.PlayOneShot(itemPickupClip);
			}
			else
				return;
		}
	}
}