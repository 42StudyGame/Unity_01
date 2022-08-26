using UnityEngine;

// 왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour {
    private float width; // 배경의 가로 길이

    //Awake메서드는 start보다 한 프레임 앞서서 실행하는 한번 실행하는 메서드이다.
    private void Awake() {
        // 가로 길이를 측정하는 처리
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;
    }

    private void Update() {
        if (transform.position.x <= -width)
        {
            Reposition();
        }
        // 현재 위치가 원점에서 왼쪽으로 width 이상 이동했을때 위치를 리셋
    }

    // 위치를 리셋하는 메서드로 update에 의해 실행된다.
    private void Reposition() {
        //현재 위치에서 오른쪽으로 가로길이 *2만큼 이동함.
        Vector2 offset = new Vector2(width * 2f, 0);
        //vector2값을 vector3에 할당하면 나머지는 0으로 변한다.
        transform.position = (Vector2)transform.position + offset;
    }
}