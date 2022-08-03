using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerInput : IInput
{
    public float Move { get; private set; } = 0;
    public float Rotate { get; private set; } = 0;
    public bool Fire { get; private set; } = false;
    public bool Reload { get; private set; } = false;
}

public partial class PlayerInput : MonoBehaviour
{
    private const string _moveAxisName = "Vertical";
    private const string _rotateAxisName = "Horizontal";
    private const string _fireButtonName = "Fire1";
    private const string _reloadButtonName = "Reload";
    private IGameManager _gameMamager = null;
    
    private void Awake()
    {
        _gameMamager = FindObjectOfType<GameManager>();
    }
    

    private void Update()
    {
        if (_gameMamager?.IsGameover ?? true)
        {
            InitializePlayer();
            return;
        }

        Move = Input.GetAxis(_moveAxisName);
        Rotate = Input.GetAxis(_rotateAxisName);
        Fire = Input.GetButton(_fireButtonName);
        Reload = Input.GetButtonDown(_reloadButtonName);
    }
    
    private void InitializePlayer()
    {
        Move = 0;
        Rotate = 0;
        Fire = false;
        Reload = false;
    }
}
