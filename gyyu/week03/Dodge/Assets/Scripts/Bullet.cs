using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;            //이동에 사용할 이동속도
    private Rigidbody _bulletRigidbody;  // 불릿 오브젝트의 리지드바디 컴포넌트
    // Start is called before the first frame update
    void Start()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
        // 게임 오브젝트에서 리지드바디 컴포넌트를 찾아 불릿 리지드바디에 할당
        _bulletRigidbody.velocity = transform.forward * speed;
        // 리지드바디의 속도 = 앞쪽 방향 * 이동속력
        Destroy(gameObject, 3f);
    }
    void OnTriggerEnter(Collider other)
    {
        // 충돌한 상대방 게임 오브젝트가 Player 태그를 가진 경우
        
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            // 상대방 게임 오브젝트에서 PlayerController 컴포넌트 가져오기
            if (playerController != null)
            {
                // 컴포넌트를 가져오는데 성공 했다면 상대방 PlayerController 컴포넌트의 Die() 메서드 실행
                playerController.Die();
            }
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject, 0);
        }
    }
}
