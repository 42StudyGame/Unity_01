using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI관련 라이브러리
using UnityEngine.SceneManagement;//씬 관리 관련 라이브러리
public class Bullet : MonoBehaviour
{
    public float speed = 8f;//탄알 이동 속력
    private Rigidbody bulletRigidbody;//이동에 사용할 리자드 바디 컴포넌트
    public GameManager gameManager;
    void Start()
    {//게임 오브젝트에서 Rigidbody 컴포넌트를 찾아 bulletRigidbody에 할당
        bulletRigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();//이부분
        bulletRigidbody.velocity = transform.forward * speed;
        Destroy(gameObject,3f);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController playercontroller = other.GetComponent<PlayerController>();
           // GameManager gameManager = FindObjectOfType<GameManager>();//이부분
            //상대방으로부터 PlayerController컴포넌트를 가져오는데 성공했다면?
            if (playercontroller != null)
            {
                playercontroller.life--;
                gameManager.lifenum = playercontroller.life;//이부분
                if (playercontroller.life < 1)    
                    playercontroller.Die();
                
            }
        }
    }
}
