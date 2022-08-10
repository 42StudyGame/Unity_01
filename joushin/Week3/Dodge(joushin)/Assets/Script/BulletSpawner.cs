using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;//탄알을 생성할 때 사용할 원본 프리팹
    public float spawnRateMin = 0.5f; //새 탄알 생성하는 시간의 최소값
    public float spawnRateMax = 3f;//새 탄알을 생성하는데 걸리는 시간의 최댓값

    private Transform target;//조준할 대상의 트랜스폼 컴포넌트
    private float spawnRate; // 다음 탄알을 생성할 때까지 기다릴 시간 Max에서 Min사이 랜덤값으로 생성됨
    private float timeAfterSpawn; //최근 생성시점에서 지난 시간
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnRate)
        {
            //누적 시간 리셋
            timeAfterSpawn = 0f;
            //복사본을 transform위치와 회전방향으로 생성한다(총알생성기)
            GameObject bullet
                = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.LookAt(target);//생성된 오브젝트가 정면방향이 target을 향하도록 만든다.
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
