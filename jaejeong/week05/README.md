# 5주차: 5부 유니런 (424p - 552p)

- 11장 유니런: 플레이어 제작
- 12장 유니런: 배경 스크롤링과 게임 매니저
- 13장 유니런: 발판 반복 생성과 게임 완성

# 과제

- 5부 유니런 실습한 프로젝트를 github에 올리고 실행 영상 올리기

[[영상 링크]](https://drive.google.com/file/d/1hgjKBoDHcTM5x3OEqT2KuSCtEKRk6ab0/view?usp=sharing)

# 유니런 업데이트

## 벽에 부딪쳤을 때 캐릭터가 밀려나는 현상

[[영상 링크]](https://drive.google.com/file/d/1PH7gD6o1sAzT0iZ8J8Z3rMX8SRBujImR/view?usp=sharing)

영상에서 보이는 것처럼 캐릭터가 발판의 옆면에 부딪히면 점점 밀려나며, 카메라 밖까지 나가지기도 한다. 이 문제를 해결하기 위해 캐릭터가 발판의 옆면에 부딪히면 배경과 발판이 잠시 멈추도록 하려고 한다.

> 기존 scrollingObject.cs
> 

```csharp
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f; // 이동 속도

    private void Update()
    {
        if (!GameManager.instance.isGameover)
        {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
				}
    }
}
```

> 수정한 scrollingObject.cs
> 

```csharp
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f; // 이동 속도
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
```

### bool 타입의 canMove 변수 추가

캐릭터가 발판의 옆면에 닿았을 때 발판이 이동할 수 없게 하기 위해 canMove 변수를 추가했다. static으로 선언한 이유는 한 발판에 닿았을 때 모든 발판과 배경이 움직이지 않게 하기 위함이다.

### onCollisionEnter2D()와 onCollisionExit2D() 메소드 활용

캐릭터와 발판의 윗면이 부딪혔는지 판단하는 코드에서 아이디어를 그대로 가져왔다. 캐릭터와 발판 윗면의 충돌 지점은 방향벡터가 (0, 1)에 가깝지만, 캐릭터와 발판 옆면의 충돌 지점은 방향벡터가 (1, 0)에 가깝다.

### 하지만..

발판 바로 옆에서 정면으로 부딪히는 경우엔 기대했던 대로 해결이 되었다. 하지만 발판과 캐릭터가 애매하게 대각선으로 만나는 경우엔 여전히 캐릭터가 발판에게 밀려난다.

## 최고 기록 저장 기능 추가

Dodge 만들 때 했던 방법과 같다.

## 업데이트 이후 플레이 영상

[[영상 링크]](https://drive.google.com/file/d/1wgGDigX5jWjktlyQ_XMPv3ubZByqLp4k/view?usp=sharing)