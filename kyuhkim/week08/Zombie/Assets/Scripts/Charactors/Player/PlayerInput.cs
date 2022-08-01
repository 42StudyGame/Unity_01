using Photon.Pun;
using UnityEngine;

public partial class PlayerInput : IInput
{
    public float StraightStep { get; private set; } = 0;
    public float Rotate { get; private set; } = 0;
    public float SideStep { get; private set; } = 0;
    public bool Fire { get; private set; } = false;
    public bool Reload { get; private set; } = false;
}

public partial class PlayerInput : MonoBehaviourPun
{
    private const string _moveAxisName = "Vertical";
    private const string _slideAxisName = "Slide";
    private const string _rotateAxisName = "Horizontal";
    private const string _fireButtonName = "Fire1";
    private const string _reloadButtonName = "Reload";
    private IGameManager _gameMamager = null;
    // private Vector3 _mousePrevious;
    
    private void Awake()
    {
        // _mousePrevious = Input.mousePosition;
        _gameMamager = FindObjectOfType<GameManager>();
    }
    

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        if (_gameMamager?.IsGameover ?? true)
        {
            InitializePlayer();
            return;
        }
        
        StraightStep = Input.GetAxis(_moveAxisName);
        Rotate = Input.GetAxis(_rotateAxisName);
        SideStep = Input.GetAxis(_slideAxisName);
        Fire = Input.GetButton(_fireButtonName);
        Reload = Input.GetButtonDown(_reloadButtonName);

        // _mousePrevious = Input.mousePosition;
    }
    
    private void InitializePlayer()
    {
        StraightStep = 0;
        Rotate = 0;
        SideStep = 0;
        Fire = false;
        Reload = false;
    }
}
