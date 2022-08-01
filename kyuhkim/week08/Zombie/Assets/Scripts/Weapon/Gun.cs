using System;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Gun : IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(ammoRemain);
            stream.SendNext(magAmmo);
            stream.SendNext(state);
        }
        else
        {
            ammoRemain = (int)stream.ReceiveNext();
            magAmmo = (int)stream.ReceiveNext();
            state = (State)stream.ReceiveNext();
        }
    }
}

public partial class Gun : IWeapon
{
    public void Fire()
    {
        if (!State.Ready.Equals(state) 
            || !(Time.time >= _lastFireTime + gunData.timeBetFire))
        {
            return;
        }
        
        _lastFireTime = Time.time;
        Shot();
    }

    public bool Reload()
    {
        if (State.Reloading.Equals(state) 
            || ammoRemain <= 0 
            || magAmmo >= gunData.magCapacity)
        {
            return false;
        }

        StartCoroutine(ReloadRoutine());
        
        return true;
    }

    public int RefillableCount
    {
        get => ammoRemain;
        set => ammoRemain = value;
    }

    public int ChargedCount
    {
        get => magAmmo;
        private set => magAmmo = value;
    }
}

public partial class Gun : MonoBehaviourPun
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }

    private State state { get; set; }
    public Transform fireTransform;
    public ParticleSystem muzzleFlashEffect;
    public ParticleSystem shellEjectEffect;
    private LineRenderer _bulletLineRenderer;
    private AudioSource _gunAudioPlayer;

    public GunData gunData;
    private const float _fireDistance = 50f;

    public int ammoRemain = 100;
    public int magAmmo;

    private float _lastFireTime;

    [PunRPC]
    public void AddAmmo(int ammo)
    {
        ammoRemain += ammo;
    }
    
    private void Awake()
    {
        _gunAudioPlayer = GetComponent<AudioSource>();
        _bulletLineRenderer = GetComponent<LineRenderer>();

        _bulletLineRenderer.positionCount = 2;
        _bulletLineRenderer.enabled = false;
    }

    private void OnEnable()
    {
        ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.magCapacity;

        state = State.Ready;
        _lastFireTime = 0;
    }

    [PunRPC]
    private void ShotProcessOnServer()
    {
        Vector3 hitPosition;

        Physics.Raycast(fireTransform.position, fireTransform.forward, out var hit, _fireDistance);

        if (hit.collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.OnDamage(gunData.damage, hit.point, hit.normal);
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = fireTransform.position + fireTransform.forward * _fireDistance;
        }
        
        photonView.RPC("ShowEffectProcessOnClients", RpcTarget.All, hitPosition);

    }

    [PunRPC]
    private void ShowEffectProcessOnClients(Vector3 hitPosition)
    {
        StartCoroutine(ShotEffect(hitPosition));
    }
    
    private void Shot()
    {
        // Vector3 hitPosition;
        //
        // Physics.Raycast(fireTransform.position, fireTransform.forward, out var hit, _fireDistance);
        //
        // if (hit.collider.TryGetComponent(out IDamageable damageable))
        // {
        //     damageable.OnDamage(gunData.damage, hit.point, hit.normal);
        //     hitPosition = hit.point;
        // }
        // else
        // {
        //     hitPosition = fireTransform.position + fireTransform.forward * _fireDistance;
        // }
        //
        // StartCoroutine(ShotEffect(hitPosition));

        photonView.RPC("ShotProcessOnServer", RpcTarget.MasterClient);
        
        --magAmmo;

        if (magAmmo <= 0)
        {
            state = State.Empty;
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();
        shellEjectEffect.Play();
        
        _gunAudioPlayer.PlayOneShot(gunData.shotClip);
        
        _bulletLineRenderer.SetPosition(0, fireTransform.position);
        _bulletLineRenderer.SetPosition(1, hitPosition);

        _bulletLineRenderer.enabled = true;
        yield return new WaitForSecondsRealtime(0.03f);
        _bulletLineRenderer.enabled = false;
    }

    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        _gunAudioPlayer.PlayOneShot(gunData.reloadClip);
        var ammoToFill = Mathf.Min(gunData.magCapacity - magAmmo, ammoRemain);
        ammoRemain -= ammoToFill;

        yield return new WaitForSecondsRealtime(gunData.reloadTime);
        
        magAmmo += ammoToFill;
        state = State.Ready;
    }
}
