using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도
	private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
	}
    private void FixedUpdate()
    {
        Rotate();
        Move();
        
        playerAnimator.SetFloat("Move", playerInput.verticalMove + playerInput.horizontalMove);
    }
    private void Move()
    {
        Vector3 moveDistance = transform.forward * (playerInput.verticalMove * moveSpeed * Time.deltaTime)
			+ transform.right * (playerInput.horizontalMove * moveSpeed * Time.deltaTime);
		playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }
    private void Rotate()
    {
        float turn = playerInput.rotate * (rotateSpeed * Time.deltaTime);
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, turn, 0f);
    }
}