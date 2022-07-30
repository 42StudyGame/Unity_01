using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : LivingEntity
{
    public LayerMask whatIsTarget;

    private LivingEntity _targetEntity;
    private NavMeshAgent _navMeshAgent;

    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _hitSound;

    private Animator _zombieAnimator;
    private AudioSource _zombieAudioPlayer;
    private Renderer _zombieRenderer;

    private float _damage = 20f;
    private float _timeBetAttack = .5f;
    private float _lastAttackTime;
    private Collider[] _livingTargetArray;


    
    private bool HasTarget => _targetEntity != null && !_targetEntity.IsDead;
    // {
    //     get 
    //     {
    //         if (null != _targetEntity && !_targetEntity.IsDead)
    //         {
    //             return true;
    //         }
    //         return false;
    //     }
    // }

    private void Awake()
    {
        _livingTargetArray = new Collider[4];
        
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _zombieAnimator = GetComponent<Animator>();
        _zombieAudioPlayer = GetComponent<AudioSource>();

        _zombieRenderer = GetComponentInChildren<Renderer>();
    }

    public void Setup(ZombieData zombieData)
    {
        startingHealth = zombieData.health;
        Health = zombieData.damage;
        _damage = zombieData.damage;
        _navMeshAgent.speed = zombieData.speed;
        _zombieRenderer.material.color = zombieData.skilColor;
    }

    private void Start()
    {
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        while (!IsDead)
        {
            if (HasTarget)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_targetEntity.transform.position);
            }
            else
            {
                _navMeshAgent.isStopped = true;
                Physics.OverlapSphereNonAlloc(transform.position, 20f, _livingTargetArray, whatIsTarget);

                foreach (var target in _livingTargetArray)
                {
                    if (!target.TryGetComponent(out LivingEntity living))
                    {
                        continue;
                    }
                    
                    _targetEntity = living;
                    break;
                }

            }
            yield return new WaitForSecondsRealtime(.25f);
        }
    }

    private void Update()
    {
        _zombieAnimator.SetBool("HasTatget", HasTarget);
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (IsDead)
        {
            return;
        }

        _hitEffect.transform.position = hitPoint;
        _hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
        _hitEffect.Play();
        _zombieAudioPlayer.PlayOneShot(_hitSound);
        
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    protected override void Die()
    {
        base.Die();

        var zombieCollider = GetComponents<Collider>();
        foreach (var item in zombieCollider)
        {
            item.enabled = false;
        }

        _navMeshAgent.isStopped = true;
        _navMeshAgent.enabled = false;
        
        _zombieAnimator.SetTrigger("Die");
        _zombieAudioPlayer.PlayOneShot(_deathSound);
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsDead
            || !(Time.time >= _lastAttackTime + _timeBetAttack) 
            || !other.TryGetComponent(out LivingEntity target)
            || !target.Equals(_targetEntity))
        {
            return;
        }

        _lastAttackTime = Time.time;
        target.OnDamage(_damage,
            other.ClosestPoint(transform.position), 
            transform.position - other.transform.position);
    }
}
