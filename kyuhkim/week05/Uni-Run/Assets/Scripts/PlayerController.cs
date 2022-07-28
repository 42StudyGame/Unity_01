using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 700f;

    private int _jumpCount = 0;
    private bool _isGround = false;
    private bool _isDead = false;

    private Rigidbody2D _playerRigidbody;
    private Animator _animator;
    private AudioSource _playerAudio;
    private static readonly int Grounded = Animator.StringToHash(k_grounded);

    private const string k_grounded = "Grounded";
    private const string k_die = "Die";
    private const string t_dead = "Dead";

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && _jumpCount < 2)
        {
            ++_jumpCount;
            _playerRigidbody.velocity = Vector2.zero;
            _playerRigidbody.AddForce(Vector2.up * jumpForce);
            _playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && _playerRigidbody.velocity.y > 0)
        {
            _playerRigidbody.velocity *= .5f;
        }
        
        _animator.SetBool(k_grounded, _isGround);
    }

    private void Die()
    {
        _animator.SetTrigger(k_die);
        _playerAudio.clip = deathClip;
        _playerAudio.Play();

        _playerRigidbody.velocity = Vector2.zero;
        _isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(t_dead) && !_isDead)
        {
            Die();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts[0].normal.y > .7f)
        {
            _jumpCount = 0;
            _isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isGround = false;
    }
}
