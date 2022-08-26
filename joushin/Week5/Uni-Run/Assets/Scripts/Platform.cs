using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour {
    public GameObject[] obstacles; // 장애물 오브젝트들 빈 배열로 선언시?
    private bool stepped = false; // 플레이어 캐릭터가 밟았었는가

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable() {//컴포넌트가 활성 화 될 때마다 실행되는 이벤트 메서드
        // 발판을 리셋하는 처리
        stepped = false;
        for (int i = 0; i < obstacles.Length; i++)
        {//장애물의 갯수를 랜덤하게 변경시킨다.
            if (Random.Range(0, 3) == 0)//반환값이 0일 확률은 1/3이다.
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // 플레이어 캐릭터가 자신을 밟았을때 점수를 추가하는 처리
        if (collision.collider.tag == "Player" && !stepped)
        {
            //점수를 추가하고 밟힘상태를 참으로 변경
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }
}