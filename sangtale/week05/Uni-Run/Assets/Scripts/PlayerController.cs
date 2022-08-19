using UnityEngine;

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

   private void Start()
   {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
   }

   private void Update()
   {
        if (isDead)
        {
            return ;
        }
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            // 점프 직전에 속도를 (0, 0)으로 변경
            playerRigidbody.velocity = Vector2.zero;
            // 리지드바디에 위쪽으로 힘 가하기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            // 사운드 재생
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // 마우스 왼쪽 버튼을 떼면 && 속도의 y값이 양수라면(상승 중)
            // 현재 속도를 절반
            playerRigidbody.velocity *= 0.5f;
        }
        animator.SetBool("Grounded", isGrounded);
   }

   private void Die()
   {
        // 애니메이터 Trigger 변경
        animator.SetTrigger("Die");

        // 오디오 소스에 할당된 클립을 변경
        playerAudio.clip = deathClip;

        // 사망 효과음 재생
        playerAudio.Play();

        // 속도를 0
        playerRigidbody.velocity = Vector2.zero;

        // 사망 상태 변경
        isDead = true;
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
        // 충돌한 상대방의 태그가 Dead이며 아직 사망하지 않았다면 Die();
        if (other.tag == "Dead" && !isDead)
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
        // 어떤 콜라이더에서 떼어진 경우 false로 변경
        isGrounded = false;
   }
}