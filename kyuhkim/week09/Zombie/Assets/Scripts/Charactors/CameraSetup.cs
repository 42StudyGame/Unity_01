using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CameraSetup : MonoBehaviourPun
{
    [SerializeField] private Transform fpsPin;
    private CinemachineVirtualCamera[] _cam;

    private void Awake()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        _cam = FindObjectsOfType<CinemachineVirtualCamera>();
    }
    
    private void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        foreach (var item in _cam)
        {
            item.Follow = fpsPin;
            item.LookAt = fpsPin;
        }
    }

    private void ToggleCam()
    {
        foreach (var item in _cam)
        {
            item.Priority++;
            item.Priority %= 2;
        }
    }
    
    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleCam();
        }
    }
}
