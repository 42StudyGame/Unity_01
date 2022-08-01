using Photon.Pun;
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

    [PunRPC]
    public override void Restore(float changeAmount)
    {
        base.Restore(changeAmount);
        _healthSlider.value = Health;
    }

    [PunRPC]
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
        
        Invoke("Respawn", 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDead || !other.TryGetComponent(out IItem item))
        {
            return;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            item.Use(gameObject);    
        }
        
        _playerAudioPlayer.PlayOneShot(_itemPickupClip);
    }

    public void Respawn()
    {
        if (photonView.IsMine)
        {
            var position = Random.insideUnitSphere * .5f;
            position.y = 0;

            transform.position = position;

            // OnEnable()를 바로 호출하는 방법도 있겠지만, 누락시킨 사이클이 생길수 있으므로, 아래 방법을 그대로 사용하는편이 좋을듯.
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}
