using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //리지디바디선언
    private Rigidbody _playerRigidbody;
    //속도 선언 (에디터에서 편집 가능 <public>)
    public float speed = 8f;
    //목숨 설정 (에디터에서 편집 가능 <public>)
    public float life = 3;
    void Start()
    {
        //리지디바디 할당
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //GetAxis 입력은 1,-1 아니면 0 컨트롤러를 사용하면 0과의 사이값 가능
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //속도 인자를 적용
        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        //플레이어의 리지디바디의 벨로시티(운동값)을 새 vector3로 설정
        _playerRigidbody.velocity = new Vector3(xSpeed, 0f, zSpeed);
    }

    public void Die()
    {
        life--;
        Debug.Log("current life :" + life);
        //목숨이 0 이하일 경우 오브젝트 비활성화
        if (life <= 0)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EndGame();
            gameObject.SetActive(false);
        }
    }
}
