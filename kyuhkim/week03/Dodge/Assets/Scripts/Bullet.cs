using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    private const float DestoryDuration = 3f;
    private Rigidbody _bulletRigidbody;
    
    private void Start()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
        _bulletRigidbody.velocity = transform.forward * speed;
        Destroy(gameObject, DestoryDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            player.Die();
        }
    }
}
