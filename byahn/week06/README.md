# Week06

## 6주차 내용
- 6주차: 6부 좀비 서바이버 14장, 15장(554p - 684p)
    - 여기서 부터는 난이도가 있는 편이므로 2주차에 걸쳐 진행합니다
    - 14장 좀비 서바이버: 레벨 아트와 플레이어 준비
    - 15장 좀비 서바이버: 총과 슈터
    - 과제
        - 6부 좀비 서바이버 15장 까지 작성한 프로젝트를 github에 올리고 실행 영상 올리기

## 과제 1
- 과제
    - 6부 좀비 서바이버 15장 까지 작성한 프로젝트를 github에 올리고 실행 영상 올리기
        - 7주차에 업로드

## 내용 정리
### 14장 좀비 서바이버
#### 다루는 내용
	- 패키지 매니저
	- 라이트 설정으로 씬의 전반적인 색 분위기를 조절하는 방법
	- 라이트맵과 글로벌 일루미네이션
	- 여러 애니메이션 클립을 섞어 사용하는 방법
	- 애니메이션을 특정 신체 부위에만 적용하는 방법
	- 플레이어의 입력과 플레이어 캐릭터 움직임 구현
	- 시네머신으로 자동 추적 카메라 만들기

- 라이트맵
	- 라이팅은 연산 비용이 비싸기 때문에 유니티는 라이팅 데이터 애셋을 사용한다. 라이트맵은 라이팅 데이터 애셋에 포함된 주요 데이터 중 하나이다.
	- 라이트맵은 오브젝트가 빛을 받았을 때 어떻게 보일지 미리 그려둔 텍스처이다.
	- 라이트맵을 구워두면 오브젝트가 빛을 받았을 때 실시간 라이팅 연산 대신 오브젝트 표면에 라이트맵을 씌워서 빛 효과를 표현한다.
	- 라이트맵을 굽는 처리는 부하가 크고 시간이 오래 걸린다.

- 글로벌 일루미네이션 (GI)
	- 물체의 표면에 직접 들어오는 빛뿐만 아니라 다른 물체의 표면에서 반사되어 들어온 간접광까지 표현한다.
	- 매우 높은 처리량을 요구한다.
	- 실시간 글로벌 일루미네이션
		- 빛의 세기와 방향 등이 달라졌을 때 그 변화를 간접광에 실시간으로 반영한다.
		- 라이트맵을 여러 방향에 대해 생성하고, 여러가지 경우에 대한 빛의 예상 반사 방향과 광원의 예상 이동 경로 등의 정보를 미리 계산해서 저장하여 적은 비용으로 추측할 수 있다.
		- 미리 계산해야 하는 정보가 있으므로 라이팅 데이터 애셋을 구워야 한다. 따라서 '미리 계산된 실시간 GI'라고 부르기도 한다.

	- 베이크된 글로벌 일루미네이션
		- 고정된 빛에 의한 간접광들을 라이트맵으로 구워 게임 오브젝트 위에 미리 입힌다.
		- 반영된 간접광 효과는 게임 도중에 실시간으로 변하지 않는다.
		- 베이크된 간접(Baked Indirect) 모드
			- 간접광만 구워서 미리 계산
			- 직사광과 그림자는 실시간으로 처리
		- 섀도우마스크(Shadowmask) 모드
			- 간접광을 위한 라이트맵 이외에 그림자 맵(섀도우마스크 맵)을 추가로 구워서 사용
			- 실시간 그림자와 미리 구워진 그림자가 자연스럽게 합성됨
		- 감산(Subtractive) 모드
			- 간접광, 직사광, 그림자까지 하나의 라이트맵에 모두 구워버림
			- 실시간 그림자와 미리 구워진 그림자가 자연스럽게 합성되지 않음
			- 가장 오버헤드가 적음(성능이 제일 좋음)

- 라이트 컴포넌트의 모드
	- 베이크됨(Baked) 모드 : 빛을 미리 라이트 맵에 굽는다.
		- 베이크됨으로 설정된 라이트는 베이크된 GI에 반영된다.
		- 라이트를 굽는시점에는 없던 게임 오브젝트, 게임 도중 움직일 수 있는 동적 게임 오브젝트에는 빛을 비추거나 그림자를 그릴 수 없다.
	- 실시간(Realtime) 모드 : 실시간으로 빛을 연산한다.
		- 런타임에 실시간 라이트의 설정이나 위치가 변겨오디면 오브젝트에 반영된다.
		- 실시간으로 움직이거나 생성되는 오브젝트의 그림자도 그릴 수 있다.
		- 베이크 된 라이트에 비해 빛의 표현이나 그림자의 질감이 떨어질 수 있다.
		- 실시간 GI가 활성화된 경우에는 실시간 라이트도 간접광을 표현할 수 있다.
	- 혼합(Mixed) 모드 : 실시간과 베이크됨 사이의 동작을 가진다.
		- 가볍거나 동적 게임 오브젝트에만 적용할 연산은 실시간, 무겁거나 정적 게임 오브젝트에만 적용할 연산은 베이크된 라이트로 동작한다.

- 리지드 바디
	- Angular Drag(각 항력): 회전에 대한 마찰력. 값을 높일 수록 물체가 잘 회전하지 않는다.

- 애니메이터 컴포넌트
	- Apply Root Motion: 게임 오브젝트의 위치와 회전을 애니메이션이 제어하도록 허용하는 옵션
	
- 애니메이터 컨트롤러
	- 레이어를 여러 개 사용하여 애니메이션 상태가 게임 오브젝트 하나에 중첩되게 할 수 있다.
	- 레이어를 두 개 이상 만들면 각 레이어에서 재생하는 애니메이션은 위에서 아래 순서로 덮어쓰기 방식으로 적용된다. (위에 것이 먼저 실행 된후 아래 것이 덧씌워져 실행 됨)

- C# : 프로퍼티(get set)
	- 자동 변환
	- 안전성 증가
	- 접근자 개별 설정

### 15장 좀비 서바이버
#### 다루는 내용
	- C# 인터페이스를 사용한 '느슨한 커플링'
	- 슈터 게임의 총 제작 방법
	- 라인 렌더러를 사용해 광선 그리기
	- 레이캐스트를 사용해 탄알 발사 구현하기
	- 코루틴을 사용해 대기 시간 삽입하기
	- IK를 사용해 총을 잡도록 애니메이션 변경하기

 - 스크립터블 오브젝트
	- 여러 오브젝트가 사용할 데이터를 유니티 에셋 형태로 저장할 수 있는 타입
	- MonoBehaviour를 상속받지 않고 ScriptableObject를 상속 받는다.
	- 여러 오브젝트가 공유하여 사용할 데이터를 에셋 형태로 분리, 데이터를 유니티 인스펙터 창에서 편집 가능한 형태로 관리해야하는 경우에 유용하다.
	- 클래스 상단에 다음과 같은 특성을 추가하여 스크립터블 오브젝트 타입의 에셋을 생성하는 메뉴를 만들 수 있다.
	```cs
		[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]
	```

- 코루틴(coroutine) 메서드
	- 대기 시간을 가질 수 있는 메서드
	- IEnumerator 타입을 반환해야 한다.
	- 처리가 일시 대기할 곳에 yield 키워드를 명시해야 한다.
	- 코루틴의 문법
		- 초단위로 쉬기 : yield return new WaitForSeconds(시간);
		- 한 프레임만 쉬기 : yield return null;

- 레이캐스트(Raycast)
	- 보이지 않는 광선을 쐈을 때 다른 콜라이더와 충돌하는지 검사하는 처리
	- 충돌하면 true, 충돌하지 않으면 false
	```cs
		Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance);
	```
	- Vector3 origin : 레이의 시작점
	- Vector3 direction : 레이의 방향
	- RaycastHit hitInfo : 레이가 충돌한 경우 hitInfo에 자세한 충돌 정보가 채워짐
		- out 키워드로 입력된 변수는 메서드 내부에서 변경된 사항이 반영된 채 되돌아온다. (C++의 reference와 비슷)
		- ref 키워드는 out 키워드와 비슷하지만 변수의 값이 이전에 이미 할당이 되어있어야 사용할 수 있다.
	- float maxDistance : 레이 충돌을 검사할 최대 거리

- FK(Forward Kinematics)
	- 부모 조인트에서 자식 조인트 순서로 움직임을 적용한다.
	- 큰 단위의 관절에서 세부적인 관절 순서로 움직임을 적용한다.
	- 어깨 &rarr; 팔 &rarr; 손

- IK(Inverse Kinematics)
	- 자식 조인트의 위치를 먼저 결정하고 부모 조인트가 거기에 맞춰서 변형된다.
	- 손의 위치를 먼저 결정하고 &rarr; 팔 &rarr; 어깨 순서로 움직인다.