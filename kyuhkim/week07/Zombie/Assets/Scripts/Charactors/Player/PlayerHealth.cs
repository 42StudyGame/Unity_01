using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private AudioClip _hitClip;
    [SerializeField] private AudioClip _itemPickupClip;

    private AudioSource _playerAudioPlayer;
    private Animator _playerAnimator;

    private PlayerMovement _playerMovement;
    private PlayerShooter _playerShooter;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerAudioPlayer = GetComponent<AudioSource>();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerShooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        _healthSlider.gameObject.SetActive(true);
        _healthSlider.maxValue = startingHealth;
        _healthSlider.value = Health;

        _playerMovement.enabled = true;
        _playerShooter.enabled = true;
    }

    public override void Restore(float changeAmount)
    {
        base.Restore(changeAmount);
        _healthSlider.value = Health;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (IsDead)
        {
            return;
        }
            
        base.OnDamage(damage, hitPoint, hitDirection);
        _playerAudioPlayer.PlayOneShot(_hitClip);
        _healthSlider.value = Health;
    }

    protected override void Die()
    {
        base.Die();
        
        _healthSlider.gameObject.SetActive(false);
        _playerAudioPlayer.PlayOneShot(_deathClip);
        _playerAnimator.SetTrigger("Die");

        _playerMovement.enabled = false;
        _playerShooter.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDead || !other.TryGetComponent(out IItem item))
        {
            return;
        }
        
        item.Use(gameObject);
        _playerAudioPlayer.PlayOneShot(_itemPickupClip);
    }
}
