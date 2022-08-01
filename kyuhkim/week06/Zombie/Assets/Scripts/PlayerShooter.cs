using Unity.VisualScripting;
using UnityEngine;

public partial class PlayerShooter : IShooter
{
    public IWeapon Weapon { get; set; }
    public IInput Input { get; set; }
}

public partial class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform _gunPivot = null;
    [SerializeField] private Transform _leftHandMount = null;
    [SerializeField] private Transform _rightHandMount = null;
    private Animator _playerAnimator = null;
    private GameManager _gameManager = null;
    // private UIManager _uiManager = null;

    private void Awake()
    {
        Input = GetComponent<IInput>();
        _playerAnimator = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
        // _uiManager = FindObjectOfType<UIManager>();
        Weapon = _gunPivot.GetComponent<IWeaponHolder>().Weapon;
    }

    private void OnEnable()
    {
        _gunPivot.gameObject.SetActive(true);
        // Gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _gunPivot.gameObject.SetActive(false);
        // Gun.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.Fire)
        {
            Weapon.Fire();
        }
        else if (Input.Reload)
        {
            if (Weapon.Reload())
            {
                _playerAnimator.SetTrigger("Reload");
            }
        }
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (Weapon != null && _gameManager != null)
        {
            _gameManager.UpdateAmmoText(Weapon.ChargedCount, Weapon.RefillableCount);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Hand();
        // Foot();
    }

    private void Hand()
    {
        _gunPivot.position = _playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
        
        _playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        _playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        _playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandMount.position);
        _playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandMount.rotation);
        
        _playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        _playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        _playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandMount.position);
        _playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandMount.rotation);
    }

    private void Foot()
    {
        _gunPivot.position = _playerAnimator.GetIKHintPosition(AvatarIKHint.RightKnee);
        
        _playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        _playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        _playerAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, _leftHandMount.position);
        _playerAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, _leftHandMount.rotation);
        
        _playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        _playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
        _playerAnimator.SetIKPosition(AvatarIKGoal.RightFoot, _rightHandMount.position);
        _playerAnimator.SetIKRotation(AvatarIKGoal.RightFoot, _rightHandMount.rotation);
    }
}