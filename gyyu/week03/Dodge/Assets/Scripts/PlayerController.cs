using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody; //이동에 사용할 리드디 컴포넌트
    public float speed = 8f; // 이동 속력

    void Start()
    {
        // 게임 오브젝트에서 Rigidbody 컴포넌트를 찾아 playerRigidbody에 할당
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 수평축과 수직축의 입력값을 감지하여 저장
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //실제 이동 속도를 입력값과 이동 속력을 사용해 결정
        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        // Vector3 속도를 (xSpeed, 0, zSpeed)로 생성
        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);
        // 리지드바디의 속도에 newVelocity 할당
        _playerRigidbody.velocity = newVelocity;
    }

    public void Die()
    {
        gameObject.SetActive(false); // 자신의 게임 오브젝트를 비활성화 = 오브젝트가 안보이게 된다.
		
		GameManager gameManager = FindObjectOfType<GameManager>();
		gameManager.EndGame();
    }
}
