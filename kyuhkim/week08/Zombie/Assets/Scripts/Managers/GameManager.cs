using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_score);
        }
        else
        {
            _score = (int)stream.ReceiveNext();
            _uiManager.UpdateScoreText(_score);
        }
    }
}

public partial class GameManager : IGameManager
{
    public bool IsGameover { get; private set; }

    public void AddScore(int score)
    {
        if (IsGameover)
        {
            return;
        }
        
        _score += score;
        _uiManager.UpdateScoreText(_score);
    }
}

public partial class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPrefab;
    private int _score = 0;
    private UIManager _uiManager;
    private float _leaveStamp = 0;
    private const float LeaveSpeed = 0.5f;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        var position = Random.insideUnitSphere * .5f;
        position.y = 0;

        PhotonNetwork.Instantiate(_playerPrefab.name, position, Quaternion.identity);
    }

    private void Update()
    {
        if (!Input.anyKeyDown)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && IsLeave())
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    private bool IsLeave()
    {
        if (Time.time < _leaveStamp + LeaveSpeed)
        {
            return true;
        }
        
        _leaveStamp = Time.time;
        return false;
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
