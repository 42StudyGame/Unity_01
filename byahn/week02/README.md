# Week02

## 2주차 내용
- 2주차: 2부 C# 프로그래밍 시작하기 (126p - 217p)
    - 4장 C# 프로그래밍 시작하기
    - 5장 게임 오브젝트 제어하기
    - 과제
        - 4장, 5장의 예제 스크립트를 작성하고 github에 pull request로 제출
        - 4장 내용 중 자신이 기존에 배웠던 언어와 차이점이 뭔지 찾아서 정리하기
            - C#이 처음인 사람은 C# 언어에 대한 첫인상
        - 객체지향에 대한 이해도 정리

## 과제 1
- 4장, 5장의 예제 스크립트를 작성하고 github에 pull request로 제출

## 과제 2
- 4장 내용 중 자신이 기존에 배웠던 언어와 차이점이 뭔지 찾아서 정리하기
    - using 키워드, 상속 문법등은 C++과 비슷하게 사용하지만 전체적으로 문법 구조가 JAVA와 매우 비슷하다고 느꼈다.
	- bool
		- C/C++ : true는 1, false는 0과 같은 의미로 사용이 가능하다.
		- C# : true != 1 이며 bool 자료형에는 true와 false만 저장 가능하다.
	- 배열의 선언
		- C/C++ : int students[30];
		- C# : int[] students = new int[30];
	- 메모리 할당
		- C/C++과는 다르게 메모리 첫영역부터 차례대로 할당한다. 메모리 할당이 가능한 영역을 탐색하는 과정이 없기 때문에 속도가 훨씬 빠르다. 사용하지 않는 메모리는 가비지 컬렉션에 의해 해제되고 할당 되어있는 메모리를 재배치하여 다시 차례대로 할당될 수 있도록 한다.

## 과제 3
- 객체지향에 대한 이해도 정리
	- 객체 지향 프로그래밍은 객체(Object)를 중심으로 프로그램을 설계, 개발해 나가는 것이다. 객체 지향 프로그래밍의 가장 큰 특징은 클래스를 이용해 함수(처리 부분), 변수(데이터 부분)를 하나로 묶어 객체(인스턴스)로 만들어 사용한다는 점이다.
	- 객체 지향의 특징
		- 캡슐화: 데이터와 코드를 하나로 묶고 외부가 볼 수 없도록 한다.
		- 추상화: 객체들의 공통적인 특징을 도출하는 것 (ex: 클래스를 정의하는 것)
		- 상속: 부모 클래스의 특징을 자식 클래스가 물려받는다.
		- 다형성: 같은 이름을 가진 함수여도 다르게 동작하게 할 수 있다.
			- 오버라이딩 - 부모 클래스의 함수를 자식 클래스에서 재정의해서 사용할 수 있다.
			- 오버로딩 - 같은 이름을 가진 함수여도 매개변수를 다르게 가지면 다르게 동작하게 할 수 있다.
	- 장점
		- 코드의 재사용성이 용이하다.
		- 유지보수가 쉽다.
		- 대규모 프로젝트에 적합하다.
	- 단점
		- 처리속도가 느리다.
		- 설계 단계에서 많은 시간이 소요된다.