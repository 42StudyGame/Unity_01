using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //리지디바디선언
    private Rigidbody _bulletRigidbody;
    //목숨 설정 (에디터에서 편집 가능 <public>)
    public float speed = 8f;

    void Start()
    {
        //리지디바디 할당
        _bulletRigidbody = GetComponent<Rigidbody>();
        //transform.forward 가 vector3를 반환
        _bulletRigidbody.velocity = transform.forward * speed;
        
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            
            if (playerController != null)
            {
                playerController.Die();
            }
        }
    }
}
