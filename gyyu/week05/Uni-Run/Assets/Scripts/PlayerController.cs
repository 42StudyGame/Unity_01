using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘

   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트
   private static readonly int Grounded = Animator.StringToHash("Grounded");
   private static readonly int _Die = Animator.StringToHash("Die");

   private void Start()
   {
       playerRigidbody = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
       playerAudio = GetComponent<AudioSource>();
   }

   private void Update()
   {
       if (isDead)
           return;
       if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
           && jumpCount < 2) // GetMouseButtonDown의 인자로 0 = 왼쪽클릭 1 = 오른쪽 2 = 휠클릭
       {
           jumpCount++;
           playerRigidbody.velocity = Vector2.zero;                 // 점프직전의 속도를 0으로 변경
           playerRigidbody.AddForce(new Vector2(0, jumpForce)); // 리지드바디에 y축(위쪽)으로 jumpForce만큼 힘주기 
           playerAudio.Play(); // 오디오 소스 실행
       }
       else if ((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
                && playerRigidbody.velocity.y > 0)
       {
           playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
       }
       animator.SetBool(Grounded, isGrounded);
   }

   private void Die()
   { 
       animator.SetTrigger(_Die);
       playerAudio.clip = deathClip;
       playerAudio.Play();
       playerRigidbody.velocity = Vector2.zero;
       isDead = true;
       GameManager.instance.OnPlayerDead();
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("Dead") && !isDead)
       {
           Die();
       }
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
       // 어떤 콜라이더와 닿았으며, 충돌 표면이 위쪽을 보고 있으면
       if (collision.contacts[0].normal.y > 0.7f)
       {
           isGrounded = true;
           jumpCount = 0;
       }
   }

   private void OnCollisionExit2D(Collision2D collision)
   {
       isGrounded = false;
   }
}