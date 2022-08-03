# 4장. C# 프로그래밍 시작하기

---

### 1) 변수

- `C#`은 문자열을 저장할 때 `string` 타입을 사용한다. → s가 대문자가 아님!
- 문자열을 저장할 땐 반드시 `"` 로 묶어야 함

### 2) 스크립트 작성

1. 스크립트 생성 시 기본으로 생성되는 코드
    
    ```csharp
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class HelloCode : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
    }
    ```
    
    `using` 키워드로 사용할 라이브러리의 경로를 지정하면 해당 라이브러리에 들어 있는 코드를 가져와서 사용할 수 있다. `using` 뒤의 경로는 `namespace`라고 한다.
    
    `Strat()` 메서드는 코드 실행이 시작되는 시발점을 제공한다. 유니티에는 상황에 맞춰 자동으로 실행되는 메서드인 유니티 이벤트 메서드가 있다. `Start()` 메서드가 대표적인 유니티 이벤트로 메소드이다. `Start()` 메서드는 게임이 시작될 때 자동으로 한 번 실행된다. 
    
2. 스크립트 실행
    
    스크립트를 완성했다고 해서 코드가 동작하는 것은 아니다. 스크립트가 동작하려면 스크립트를 게임 오브젝트로 만들어야 한다. 그러기 위해선 게임 오브젝트에 스크립트를 컴포넌트로 붙여 넣어야 한다.
    

    

### 3) 코딩 기본 규칙

1. 주석
    
    주석은 컴퓨터가 처리하지 않는 영역이다. 한 줄 주석은 `//`로 여러 줄 주석은 `/* */`로 표시한다.
    
2. 콘솔 출력
    
    콘솔에 메시지를 출력할 때는 `Debug.Log()` 메서드를 사용한다. 
    

# 5장. 게임 오브젝트 제어하기

---

## Animal 오브젝트 만들기

```csharp
public class Animal
{
	public string name;
	public string sound;
	
	public void PlaySound() {
		Debug.Log(name + ":" + sound);
	}
}
```

<aside>
💡 MonoBehaviour 클래스는 게임 오브젝트의 컴포넌트로서 필요한 기능들을 제공한다. 따라서 이 클래스를 상속하지 않은 Animal 클래스는 게임 오브젝트에 컴포넌트로 추가할 수 없다.

</aside>

<aside>
💡 MonoBehaviour는 `new` 연산자로 생성이 불가능하다. 게임 오브젝트에 컴포넌트로 스크립트를 추가하는 방식으로만 오브젝트 생성이 가능하며, 컴포넌트로 추가되면서 초기화 된다.

</aside>

### Zoo 클래스

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
			Animal tom = new Animal();
			tom.name = "톰";
			tom.sound = "냐옹!";
	
			tom.PlaySound();
    }
}
```

### 얕은 복사와 깊은 복사

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
			Animal tom = new Animal();
			tom.name = "톰";
			tom.sound = "냐옹!";

			Animal jerry = new Animal(0;
			jerry.name = "제리";
			jerry.sound = "찍!";
		
			jerry = tom;
			// tom과 같은 객체를 참조하는 것이 아니라 tom의 값만 복사해서 대입
			jerry.name = "미키";

			tom.PlaySound();
			jerry.PlaySound();
			// 톰 : 냐옹!
			// 미키 : 찍!
    }
}
```

```csharp
Some a = new Some();

Some b = a;
Some c = a;
// a, b, c 모두 같은 객체의 참조값을 가지고 있음
```

### 기존에 알고 있던 언어와 `C#`의 차이

C++과 C#의 차이
- c#은 가비지 컬렉터가 메모리 관리를 해준다.
- c++의 경우 bool 형식은 정수형으로 변환이 가능하지만, c#의 경우 bool 형식은 정수형으로 변환되지 않는다.
- c++의 long 형은 32bit 이지만 c#의 long 형은 64bit이다.
- c++은 헤더 파일을 사용하지만 c#은 헤더 파일을 사용하지 않는다.
- c#에서 switch문은 case 레이블에 대해서 순차 조건 검사를 지원하지 않는다.
- c#의 문법은 c++이나 JAVA와 비슷하다고 느꼈다.


### 객체 지향에 대한 이해도 정리

객체지향은 독립적이며 ㅅ스로 동작하는 여러 객체가 모여 거대한 프로그램이 완성되는 구조를 만드는 방법이다. 

### Class

클래스와 오브젝트는 객체지향의 핵심이다. 객체지향은 `사람이 현실 세상을 보는 방식`에 가깝게 프로그램을 완성하는 것이다. 클래스는 표현하고 싶은 대상을 `추상화`하여 대상과 관련된 변수와 메서드를 정의하는 틀이다. 

### Object

클래스는 실제로 존재하는 오브젝트가 아니지만 클래스를 이용해 오브젝트를 생성할 수 있다. 클래스로 오브젝트를 실체화 하는 것을 `인스턴스화` 한다고 하며, `인스턴스화`를 이용해 생성된 오브젝트를 `인스턴스`라고 한다. 오브젝트는 인스턴스를 포함하는 개념이므로 두 단어는 혼용된다.

- 독립성
    
    하나의 원본 클래스에서 여러 개의 오브젝트를 생성할 수 있지만, 오브젝트는 서로 독립적이며 구별 가능한 실체이다.