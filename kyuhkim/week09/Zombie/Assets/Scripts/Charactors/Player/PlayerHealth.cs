using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    [SerializeField] private Slider _healthSlider = null;
    [SerializeField] private AudioClip _deathClip = null;
    [SerializeField] private AudioClip _hitClip = null;
    [SerializeField] private AudioClip _itemPickupClip = null;
    private GameManager _gameManager = null;
    
    private const int DeathPenalty = -200;
    
    private AudioSource _playerAudioPlayer = null;
    private Animator _playerAnimator = null;
    
    private PlayerMovement _playerMovement = null;
    private PlayerShooter _playerShooter = null;
    
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        
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
        
        _gameManager.AddScore(DeathPenalty);
        
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
        }
        
        // OnEnable()를 바로 호출하는 방법도 있겠지만, 누락시킨 사이클이 생길수 있으므로, 아래 방법을 그대로 사용하는편이 좋을듯.
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}

    // RPC method 'OnDamage' found on object with PhotonView 1001 but has wrong parameters. Implement as 'OnDamage(Vector3, Vector3)'. PhotonMessageInfo is optional as final parameter.Return type must be void or IEnumerator (if you enable RunRpcCoroutines).
    // UnityEngine.Debug:LogErrorFormat (UnityEngine.Object,string,object[])
    // Photon.Pun.PhotonNetwork:ExecuteRpc (ExitGames.Client.Photon.Hashtable,Photon.Realtime.Player) (at Assets/Photon/PhotonUnityNetworking/Code/PhotonNetworkPart.cs:646)
    // Photon.Pun.PhotonNetwork:OnEvent (ExitGames.Client.Photon.EventData) (at Assets/Photon/PhotonUnityNetworking/Code/PhotonNetworkPart.cs:2201)
    // Photon.Realtime.LoadBalancingClient:OnEvent (ExitGames.Client.Photon.EventData) (at Assets/Photon/PhotonRealtime/Code/LoadBalancingClient.cs:3353)
    // ExitGames.Client.Photon.PeerBase:DeserializeMessageAndCallback (ExitGames.Client.Photon.StreamBuffer) (at D:/Dev/Work/photon-dotnet-sdk/PhotonDotNet/PeerBase.cs:899)
    // ExitGames.Client.Photon.EnetPeer:DispatchIncomingCommands () (at D:/Dev/Work/photon-dotnet-sdk/PhotonDotNet/EnetPeer.cs:565)
    // ExitGames.Client.Photon.PhotonPeer:DispatchIncomingCommands () (at D:/Dev/Work/photon-dotnet-sdk/PhotonDotNet/PhotonPeer.cs:1771)
    // Photon.Pun.PhotonHandler:Dispatch () (at Assets/Photon/PhotonUnityNetworking/Code/PhotonHandler.cs:222)
    // Photon.Pun.PhotonHandler:FixedUpdate () (at Assets/Photon/PhotonUnityNetworking/Code/PhotonHandler.cs:145)
    
    // RPC method 'Restore' found on object with PhotonView 2001 but has wrong parameters. Implement as 'Restore(Single, Boolean)'. PhotonMessageInfo is optional as final parameter.Return type must be void or IEnumerator (if you enable RunRpcCoroutines).
    // UnityEngine.Debug:LogErrorFormat (UnityEngine.Object,string,object[])
    // Photon.Pun.PhotonNetwork:ExecuteRpc (ExitGames.Client.Photon.Hashtable,Photon.Realtime.Player) (at Assets/Photon/PhotonUnityNetworking/Code/PhotonNetworkPart.cs:646)
    // Photon.Pun.PhotonNetwork:OnEvent (ExitGames.Client.Photon.EventData) (at Assets/Photon/PhotonUnityNetworking/Code/PhotonNetworkPart.cs:2201)
    // Photon.Realtime.LoadBalancingClient:OnEvent (ExitGames.Client.Photon.EventData) (at Assets/Photon/PhotonRealtime/Code/LoadBalancingClient.cs:3353)
    // ExitGames.Client.Photon.PeerBase:DeserializeMessageAndCallback (ExitGames.Client.Photon.StreamBuffer) (at D:/Dev/Work/photon-dotnet-sdk/PhotonDotNet/PeerBase.cs:899)
    // ExitGames.Client.Photon.EnetPeer:DispatchIncomingCommands () (at D:/Dev/Work/photon-dotnet-sdk/PhotonDotNet/EnetPeer.cs:565)
    // ExitGames.Client.Photon.PhotonPeer:DispatchIncomingCommands () (at D:/Dev/Work/photon-dotnet-sdk/PhotonDotNet/PhotonPeer.cs:1771)
    // Photon.Pun.PhotonHandler:Dispatch () (at Assets/Photon/PhotonUnityNetworking/Code/PhotonHandler.cs:222)
    // Photon.Pun.PhotonHandler:FixedUpdate () (at Assets/Photon/PhotonUnityNetworking/Code/PhotonHandler.cs:145)
