using UnityEngine;

// 게임 오브젝트를 계속 왼쪽으로 움직이는 스크립트
public class ScrollingObject : MonoBehaviour
{
    public static float speed = 10f; // 이동 속도
    public static bool canMove = true;

    private void Update()
    {
        // 게임 오브젝트를 왼쪽으로 일정 속도로 평행 이동하는 처리
        if (!GameManager.instance.isGameover)
        {
            if (canMove)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.x > 0.7f)
        {
            canMove = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        canMove = true;
    }
}