using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public static float speed = 10f;
    public static bool canMove = true;

    private void Update()
    {
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