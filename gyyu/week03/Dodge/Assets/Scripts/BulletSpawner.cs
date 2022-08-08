using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
	public GameObject bulletPrefab;		//생성할 탄알의 원본 프리팹
	public float spawnRateMin = 0.5f;	// 최소 생성 주기
	public float spawnRateMax = 3f;		//최대 생성 주기

	private Transform _target;		// 발사할 주기
	private float _spawnRate;		// 생성주기
	private float _timeAfterSpawn;	// 최근 생성 시점에서 지난 시간

	void Start()
	{
		_timeAfterSpawn = 0f; //최근 생성 이후의 누적 시간을 0으로 초기화
		_spawnRate = Random.Range(spawnRateMin, spawnRateMax); // 탄알 생성간격을 Min, Max사이에서 랜덤 지정
		_target = FindObjectOfType<PlayerController>().transform;
		// PlayerController 컴포넌트를 가진 게임오브젝트를 찾아 조준 대상으로 설정
	}

	void Update()
	{
		// 스포너 시간 갱신
		_timeAfterSpawn += Time.deltaTime;

		// 최근 생성 시점에서부터 누적된 시간이 생성 주기보다 크거나 같다면
		if (_timeAfterSpawn >= _spawnRate) // 생성주기가 되면 게임오브젝트 생성후 방향을 target을 향하도록 회전 다음 Bullet 생성 시간랜덤지정
		{
			//최근 생성 시점 초기화
			_timeAfterSpawn = 0f;

			//Bullet 생성
			var transform1 = transform;
			GameObject bullet = Instantiate(bulletPrefab, transform1.position, transform1.rotation);
			// 생성된 Bullet 게임 오브젝트의 정면 방향을 target을 향하도록 회전
			bullet.transform.LookAt(_target);
			// 다음에 생성될 Bullet의 시간을 spawnRateMin, spawnRateMax 사이에서 랜덤 지정
			_spawnRate = Random.Range(spawnRateMin, spawnRateMax);
		}
	}
}
