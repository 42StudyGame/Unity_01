using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// 좀비 게임 오브젝트를 주기적으로 생성
public class ZombieSpawner : MonoBehaviour {

	public Zombie zombiePrefab; // 생성할 좀비 원본 프리팹

	public ZombieData[] zombieDatas; // 사용할 좀비 셋업 데이터들
	public Transform[] spawnPoints; // 좀비 AI를 소환할 위치들

	private List<Zombie> zombies = new List<Zombie>(); // 생성된 좀비들을 담는 리스트
	private int wave; // 현재 웨이브

	private void Update() {
		if (GameManager.instance != null && GameManager.instance.isGameover)
			return;
		if (zombies.Count <= 0)
			SpawnWave();
		UpdateUI();
	}

	private void UpdateUI() {
		UIManager.instance.UpdateWaveText(wave, zombies.Count);
	}

	private void SpawnWave() {
		wave++;
		int spawnCount = Mathf.RoundToInt(wave * 1.5f);

		for (int i = 0; i < spawnCount; i++)
			CreateZombie();
	}

	private void CreateZombie() {
		ZombieData zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)];
		Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
		Zombie zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
		zombie.Setup(zombieData);
		zombies.Add(zombie);
		zombie.onDeath += () => zombies.Remove(zombie);
		zombie.onDeath += () => Destroy(zombie.gameObject, 10f);
		zombie.onDeath += () => GameManager.instance.AddScore(100);
	}
}