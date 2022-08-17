# Week04

## 4주차 내용
- 4주차: 4부 공간 (360p - 421p)
    - 9장 방향, 크기, 회전
    - 10장 공간과 움직임
    - 과제
        - 4부 공간 실습한 프로젝트를 github에 올리고 동작 영상 올리기

## 과제 1
- 과제
    - 4부 공간 실습한 프로젝트를 github에 올리고 동작 영상 올리기
        - https://drive.google.com/file/d/1WmLhM5DrPYF_M8lcq5gzOkpPHbGgthLN/view?usp=sharing

## 내용 정리
### 9장 방향, 크기, 회전
- 벡터
    - 벡터의 덧셈
        - A(3, 2) + B(1,6) = (4, 8)
        - A만큼 이동한 다음 B만큼 이동한 위치를 의미할 수 있다.

    - 벡터의 뺄셈
        - B(-2, 8) - A(1, 3) = (-3, 5)
        - A에서 B까지의 방향과 거리를 의미할 수 있다.

    - 벡터의 내적
        - A · B = |A||B|cosΘ
        - A(3, 5) · B(1, 4) = 3\*1 + 5\*4 = 23
        - A와 B의 크기가 1인 방향벡터의 경우 내적 결과로 둘사이의 각도를 알 수있다.
            - 0º ~ 90º : +1 ~ 0
            - 90º ~ 180º : 0 ~ -1

    - 벡터의 외적
        - A X B = C 일때, 벡터 C는 벡터 A, B 에 모두 수직인 벡터를 구한다. 즉 A와 B를 포함하는 평면 L에 수직인 벡터이다.
        - 벡터 C는 평면 L이 바라보고 있는 방향을 의미할 수 있다.
        - 평면에 수직인 벡터를 노말벡터 또는 법선벡터라고 한다.

- 유니티의 Vector 타입
    - Vector 타입은 유니티 라이브러리 내부에 클래스가 아니라 구조체로 선언되어 있다.
    - 구조체는 클래스와 달리 참조 타입으로 동작하지 않고 값 타입으로 동작한다. 따라서 아래와 같은 경우 a의 값이 수정되지 않는다.
        ```cs
            Vector3 a = new Vector3(0, 0, 0);
            Vector3 b = a;
            b.x = 100;
        ```
    - a:(0, 0, 0), b:(100, 0, 0)
    - Vector3 연산
        - 스칼라 곱, 덧셈과 뺄셈
        ```cs
            Vector3 * 스칼라;
            (3, 6, 9) * 10 -> (30, 60, 90)
            Vector3 + Vector3;
            Vector3 - Vector3;
        ```
        - 벡터 정규화(방향벡터로 만들기)
        ```cs
            Vector3.normalized;
            (3, 3, 3) -> (0.57, 0.57, 0.57)
        ```
        - 벡터의 크기(길이)
        ```cs
            Vector3.magnitude;
            (3, 3, 3) -> 5.19...
        ```
        - 벡터의 내적
        ```cs
            Vector3.Dot(a, b);
        ```
        - 벡터의 외적
        ```cs
            Vector3.Cross(a, b);
        ```

- 쿼터니언(Quaternion)
    - 회전을 나타내는 타입
    - 짐벌락(Gimbal Lock)
        - 오일러각 체계에서 벡터를 사용해 회전을 표현하게 되면 어떤 축의 회전이 다른 축의 회전에 영향을 미친다는 사실과 세 번 나누어 축을 회전하는 방식 때문에, 특정한 경우 앞선 두번의 회전에 의해 세번째 회전의 자유도가 상실되어 세 축 중 한 축의 회전을 사용할 수 없게 되는 현상이 발생한다.
        - 회전을 세 번 나누어 실행하는 도중에 축 두 개가 겹쳐 하나의 축으로 잠금되기 때문이다.
        - 어떤 축을 90도 회전할 때 특히 자주 발생하기 때문에 아주 오래 전에 만들어진 게임들은 90도 회전을 사용하지 않고 89.9도와 같은 값으로 회전을 처리하기도 한다.
    - 쿼터니언은 한번에 회전하는 방식이기 때문에 오일러각과 달리 짐벌락 현상이 없으며 90도 회전을 제대로 표현할 수 있다.
    - 쿼터니언 내부는 복잡한 계산으로 이루어지기 때문에 컴포넌트의 회전은 Vector3로 다룬다. 이 같은 이유로 유니티는 코드 상에서 쿼터니언을 직접 생성하거나 내부를 직접 수정하는 것을 허용하지 않는다.
    - 새로운 회전 데이터 생성
        ```cs
            Quaternion.Euler(Vector3);
        ```
    - 회전을 Vector3(오일러각)로 가져오기
        ```cs
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 60, 0));

            Vector3 eulerRotation = rotation.eulerAngles;
        ```
    - 회전을 여러번 할때
        - 덧셈(+)이 아니라 곱셈(*)으로 표현한다.
        ```cs
            Quaternion a = Quaternion.Euler(30, 0, 0);
            Quaternion b = Quaternion.Euler(0, 60, 0);

            Quaternion rotation = a * b;
        ```

### 10장 공간과 움직임
- 유니티 공간
    - 전역 공간(월드 공간)
        - 월드의 중심이라는 절대 기준이 존재하는 공간
        - 게임 월드의 원점을 기준으로 위치를 측정
    - 오브젝트 공간
        - 자기 자신을 기준으로 위치를 측정
    - 지역 공간
        - 자신의 부모 오브젝트를 기준으로 위치를 측정
    - 유니티는 편의상 지역 공간과 오브젝트 공간을 합쳐서 지역 공간으로 부른다.

- 오브젝트의 이동과 회전
    - transform.Translate() 메서드
        ```cs
            // 지역 공간 기준으로 y축으로 이동 (2번째 매개변수 Space.Self는 기본값이므로 생략 가능)
            transform.Translate(new Vector3 (0, 1, 0) * Time.deltaTime, Space.Self);
            // 전역 공간 기준으로 y축으로 이동
            transform.Translate(new Vector3 (0, 1, 0) * Time.deltaTime, Space.World);
        ```
    - 벡터의 속기
        - 미리 만들어진 방향벡터들
        - Vector3.forward : new Vector3(0, 0, 1)
        - Vector3.back : new Vector3(0, 0, -1)
        - Vector3.right : new Vector3(1, 0, 0)
        - Vector3.left : new Vector3(-1, 0, 0)
        - Vector3.up : new Vector3(0, 1, 0)
        - Vector3.down : new Vector3(0, -1, 0)