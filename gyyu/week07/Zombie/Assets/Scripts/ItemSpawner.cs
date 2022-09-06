using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random; // 내비메쉬 관련 코드

// 주기적으로 아이템을 플레이어 근처에 생성하는 스크립트
public class ItemSpawner : MonoBehaviour {
	public GameObject[] items; // 생성할 아이템들
	public Transform playerTransform; // 플레이어의 트랜스폼

	public float maxDistance = 5f; // 플레이어 위치로부터 아이템이 배치될 최대 반경

	public float itemDestroyTime = 5f;
	public float timeBetSpawnMax = 7f; // 최대 시간 간격
	public float timeBetSpawnMin = 2f; // 최소 시간 간격
	private float timeBetSpawn; // 생성 간격

	private float lastSpawnTime; // 마지막 생성 시점

	private void Start() {
		timeBetSpawn = Random.Range(timeBetSpawnMax, timeBetSpawnMin);
		lastSpawnTime = 0;
	}
	private void Update() {
		if (Time.time >= timeBetSpawn + lastSpawnTime && playerTransform != null)
		{
			lastSpawnTime = Time.time;
			timeBetSpawn = Random.Range(timeBetSpawnMax, timeBetSpawnMin);
			Spawn();
		}
	}

	private	void Spawn() {
		Vector3 spawnPosition = GetRandomPointOnNavMesh(playerTransform.position, maxDistance);

		spawnPosition += Vector3.up * 0.5f;

		GameObject selectedItem = items[Random.Range(0, items.Length)];
		GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);
		
		Destroy(item, itemDestroyTime);
	}

	private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance) {
		Vector3 randomPos = Random.insideUnitSphere * distance + center;

		NavMeshHit hit;

		NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);
		return hit.position;
	}
}