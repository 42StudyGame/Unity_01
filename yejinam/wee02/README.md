# 4장 내용 중 자신이 기존에 배웠던 언어와 차이점=

* 함수와 메소드의 차이점 

  메소드라는걸 java공부할때 처음 봤었는데, 객체지향언어에서 메소드 내부에 존재하는 함수를 메소드라고 함

각주 : C#에서는 (클래스의) 함수를 ‘메서드(method)’라고 부릅니다. 함수와 메서드는 혼용할 수 있지만 통일성을 위해 다음 절부터는 C# 함수를 모두 ‘메서드’로 통일하겠습니다


* Debug.Log("캐릭터 이름 : " + characterName);

  출력할때 printf와 달리 포맷과 변수를 넘기지않고 ‘string + 변수’ 형식으로 사용

* 배열 선언의 차이
```
 int[] students = new int[5];
 int students[5];
```

# 객체지향에 대한 이해도 정리
```
public class Animal // MonoBehaviour를 상속하지 않음!
{
    // 동물에 대한 변수
    public string name;
    public string sound;

    // 울음소리를 재생하는 메서드
    public void PlaySound()
    {
        Debug.Log(name + ":" + sound);
    }
}

Animal tom = new Animal();
```
* 인스턴스화

  클래스라는 틀로 오브젝트를 실체화 하는것

  예시에서는 Animal이라는 클래스를 이용해 tom이라는 Animal 오브젝트를 인스턴스화 함

* new 연산자

  어떤 클래스의 오브젝트를 생성함 (인스턴스).

* Animal() 메소드

  Animal 클래스의 생성자로 어떻게 초기화할지 정의하는 메소드
```
jerry = tom
```
jerry오브젝트에 tom오브젝트의 값을 복사해온것이 아니라,

jerry오브젝트가 tom오브젝트가 가르키는 값을 똑같이 가르키게 된것

아직 참조타입에 대해서 이해도가 완벽하지는 않지만, c에서의 포인터 변수선언후 malloc으로 할당하는것과 비슷하게 다가와진다
```
 char* str = malloc ≒ Animal a = new Animal();  ??!?
```
참조타입이 포인터랑 비슷하다고 이해하면 되려나요..?
