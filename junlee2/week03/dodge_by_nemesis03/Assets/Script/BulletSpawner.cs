using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;
    public float spawnHight = 2f;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;
    void Start()
    {
	    //시간 초기화
	    timeAfterSpawn = 0f;
	    spawnRate = Random.Range(spawnRateMin, spawnRateMax);
	    
	    target = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
	    //시간 갱신
	    timeAfterSpawn += Time.deltaTime;
	    //플레이어를 바라보기
	    transform.LookAt(target);
	    if (timeAfterSpawn >= spawnRate)
	    {
		    //시간 초기화
		    timeAfterSpawn = 0f;

		    //bulletPrefab의 복사본을 자신의 transform.position 과 transform.rotation으로 설정
		    var transform1 = transform;
		    var position = transform1.position;
		    var rotation = transform1.rotation;
		    Vector3 spawnpoint = new Vector3(position.x, position.y + spawnHight, position.z);
		    GameObject bullet = Instantiate(bulletPrefab, spawnpoint, rotation);
		    //정면을 바라보도록 설정
		    bullet.transform.LookAt(target);
		    spawnRate = Random.Range(spawnRateMin, spawnRateMax);
	    }
    }
}
