# Week03

## 3주차 내용
- 3주차: 3부 닷지 플레이어 제작 (220p - 359p)
	- 6장 닷지: 플레이어 제작
	- 7장 닷지: 탄알 제작
	- 8장: 닷지: 게임 매니저와 UI, 최종 완성
	- 과제
		- 3부 닷지 게임의 제작 예제 프로젝트를 github에 올리고 플레이 영상도 올려보기
		- 만약 xbox controller나 다른 컨트롤러가 있는 분은 연결해서 해보세요 재밌습니다!

## 과제 1
- 3부 닷지 게임의 제작 예제 프로젝트를 github에 올리고 플레이 영상도 올려보기
	- https://drive.google.com/file/d/1ueW_ADWSX8JDc5fBaAChEHD28UidJ1uL/view?usp=sharing

## 내용 정리
- 머티리얼(Material)
	- 셰이더와 텍스처가 합쳐진 에셋으로, 오브젝트의 픽셀 컬러를 결정
	- 셰이더: 질감과 빛에 의한 반사와 굴절 등의 효과 생서
	- 텍스처: 표면에 입히는 이미지 파일
	- 알베도(Albedo): 물체가 어떤색을 반사할지 결정

- 카메라의 배경 변경
	1. Main Camera 게임 오브젝트의 Camera 컴포넌트에서 Clear Flags의 값을 Solid Color로 변경
	2. Background의 컬러필드 클릭
	3. RGB 컬러 변경

- 리지드바디 제약 설정하기
	1. 인스펙터 창에서 Rigidbody 컴포넌트의 Constraints 펼치기
	2. Freeze Position, Freeze Rotation 설정
	- 제약 옵션을 설정하면 힘이나 충돌 등 물리적인 상호작용으로 위치나 회전이 변경되는 것을 막을 수 있다.
	- 하지만 트랜스폼 컴포넌트의 위치나 회전에 새로운 값을 할당하여 위치나 회전을 변경하는 것은 막을 수 없다.

- GetComponent() 메서드
	- 원하는 타입의 컴포넌트를 자신의 게임 오브젝트에서 찾아오는 메서드
	- 찾지 못했을 때는 null을 반환

- 리지드바디의 velocity 변수
	- 오브젝트의 현재 속도를 알 수 있고, 새로운 값을 할당하여 속도를 변경할 수 있다.

- 키보드 입력
	- Input.Getkey() 메서드
		- 키보드의 특정 키의 입력을 감지할 수 있다.
	- Input.GetAxis() 메서드
		- 입력축에 대응하는 버튼의 입력을 감지하여 -1.0 부터 1.0 사이의 값을 반환한다.
		- 유니티의 입력 매니저에서 입력축을 설정 가능하다.

- Destroy(Obeject obj, float t) 메서드
	- obj 오브젝트를 t초 후에 파괴

- 충돌 이벤트 메서드
	- OnCollision 계열 : 일반 충돌
		- 충돌했을 때 서로 통과하지 않고 밀어낸다.
	- Ontirgger 계열 : 트리거 충돌
		- 두 게임 오브젝트의 콜라이더 중 최소 하나가 트리거 콜라이더라면 자동으로 실행된다. (자신이 트리거 콜라이더가 아니어도 실행된다)
		- 충돌했을 때 서로 그대로 통과한다.

- 프리팹 편집
	- 인스턴스를 통한 프리팹 편집
		- 프리팹과 연동된 게임 오브젝트에서 인스펙터 창 상단의 Overrides > Apply All을 클릭하면 변경 사항이 프리팹에 반영된다.
		- Revert All을 클릭하면 변경 사항이 취소되고 원본 프리팹과 같은 모습으로 돌아간다.
	- 프리팹 편집 모드 사용

- Random.Range() 메서드
	- Random.Range(0, 3) : 0, 1, 2 중에 하나가 int 값으로 반환됨
	- Random.Range(0f, 3f) : 0f 부터 3f 사이의 flaot 값이 반환됨

- FindObjectOfType() 메서드
	- 씬에 존재하는 모든 오브젝트를 검색하여 원하는 타입의 오브젝트를 찾아내기 때문에 처리비용이 크다. 따라서 초기에 한두 번만 실행 되는 것이 좋다.
	- 검색해서 가장 처음 나타난 오브젝트를 반환한다.
	- FindObjectsOfType() 메서드는 해당 타입의 오브젝트를 모두 찾아 배열로 반환한다.

- Time.deltaTime
	- 1프레임당 시간 간격 == Update() 메서드의 실행 주기
	- 60프레임일 때 : Time.deltaTime == 1/60

- transform.LookAt() 메서드
	- 입력받은 다른 게임 오브젝트의 트랜스폼을 바라보도록 회전시킨다.

- Instantiate() 메서드
	- 오브젝트를 복제하여 새로운 오브젝트를 생성한다.

- PlayerPrefs
	- 간단한 방식으로 어떤 수치를 로컬에 저장하고 나중에 다시 불러오는 메서드를 제공하는 유니티에 내장된 클래스
	- 키-값 단위로 데이터를 로컬에 저장한다.
	- PlayerPrefs.SetFloat(string key, float value)
	- PlayerPrefs.SetInt(string key, int value)
	- PlayerPrefs.SetString(string key, string value)
	- PlayerPrefs.GetFloat(string key)... 와 같은 형식으로 사용한다.
	- GetInt()와 GetFloat()은 저장된 값이 존재하지 않으면 기본값인 0을 반환하고 GetString()은 빈문자열 ""을 반환한다.
	- HasKey() 메서드는 해당 키로 저장된 값이 존재하면 true를 반환