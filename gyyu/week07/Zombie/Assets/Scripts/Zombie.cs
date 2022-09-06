using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI; // AI, 내비게이션 시스템 관련 코드 가져오기

// 좀비 AI 구현
public class Zombie : LivingEntity {
	public LayerMask whatIsTarget; // 추적 대상 레이어

	private LivingEntity targetEntity; // 추적 대상
	private NavMeshAgent navMeshAgent; // 경로 계산 AI 에이전트

	public ParticleSystem hitEffect; // 피격 시 재생할 파티클 효과
	public AudioClip deathSound; // 사망 시 재생할 소리
	public AudioClip hitSound; // 피격 시 재생할 소리

	private Animator zombieAnimator; // 애니메이터 컴포넌트
	private AudioSource zombieAudioPlayer; // 오디오 소스 컴포넌트
	private Renderer zombieRenderer; // 렌더러 컴포넌트

	public float damage = 20f; // 공격력
	public float timeBetAttack = 0.5f; // 공격 간격
	private float lastAttackTime; // 마지막 공격 시점
	
    // 추적할 대상이 존재하는지 알려주는 프로퍼티
	private bool hasTarget
	{
		get
		{
			if (targetEntity != null && !targetEntity.dead)
				return true;
			else
				return false;
		}
	}

	private void Awake() {
		navMeshAgent = GetComponent<NavMeshAgent>();
		zombieAnimator = GetComponent<Animator>();
		zombieAudioPlayer = GetComponent<AudioSource>();

		zombieRenderer = GetComponentInChildren<Renderer>();
	}

	public void Setup(ZombieData zombieData) {
		startingHealth = zombieData.health;
		health = zombieData.health;
		damage = zombieData.damage;
		navMeshAgent.speed = zombieData.speed;
		zombieRenderer.material.color = zombieData.skinColor;
	}

	private void Start() {
		StartCoroutine(UpdatePath());
	}

	private void Update() {
		zombieAnimator.SetBool("HasTarget", hasTarget);
	}

	private IEnumerator UpdatePath() {
		while (!dead)
		{
			if (hasTarget)
			{
				navMeshAgent.isStopped = false;
				navMeshAgent.SetDestination(targetEntity.transform.position);
			}
			else
			{
				navMeshAgent.isStopped = true;
				// 20유닛의 반지름을 가진 가상의 구를 그렸을 때 구와 겹치는 모든 콜라이더를 가져옴..?
				// 찾아오는 기준을 알고싶다. 컴포넌트 기준으로 가까운 기준인가 아니면 하이어라키창의 순서대로 찾나
				// FindObjectsOfType<>() 보다는 좀 더 최적화 된것인거 같기도..
				// whatIsTarget 레이어를 가진 콜라이더만 가져오도록 필터링
				Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);
				for (int i = 0; i < colliders.Length; i++)
				{
					LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();

					if (livingEntity != null && !livingEntity.dead)
					{
						targetEntity = livingEntity;
						break;
					}
				}
			}
			yield return new WaitForSeconds(0.25f);
		}
	}

	public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
		if (!dead)
		{
			hitEffect.transform.position = hitPoint;
			hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
			hitEffect.Play();
			
			zombieAudioPlayer.PlayOneShot(hitSound);
		}
		base.OnDamage(damage, hitPoint, hitNormal);
	}

	public override void Die() {
		base.Die();

		Collider[] zombieColliders = GetComponents<Collider>();
		for (int i = 0; i < zombieColliders.Length; i++)
			zombieColliders[i].enabled = false;
		navMeshAgent.isStopped = true;
		navMeshAgent.enabled = false;
		
		zombieAnimator.SetTrigger("Die");
		zombieAudioPlayer.PlayOneShot(deathSound);
	}

	private void OnTriggerStay(Collider other) {
		if (!dead && Time.time >= lastAttackTime + timeBetAttack)
		{
			LivingEntity attackTarget = other.GetComponent<LivingEntity>();
			if (attackTarget != null && attackTarget == targetEntity)
			{
				lastAttackTime = Time.time;
				Vector3 hitPoint = other.ClosestPoint(transform.position);
				Vector3 hitNormal = transform.position - other.transform.position;
				
				attackTarget.OnDamage(damage, hitPoint, hitNormal);
			}
		}
	}
}