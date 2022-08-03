# 변수

> C#의 변수 선언 방식
> 

```csharp
int gold = 1000;
float itemWeight = 1.34f;
bool isStoreOpen = true;
string itemName = "포션";
int hp;
hp = 200;
```

변수의 선언, 대입 방식은 C와 차이점이 없다. 다만 자료형에 차이가 몇 가지 있다. 1바이트 크기의 자료형 byte형이 생기고, char형은 2바이트로 문자를 표현하는 자료형이 되었으며, unsigned 표현 방법이 다르다.

| 데이터 형식 | 부호 | 크기 | 표현 |
| --- | --- | --- | --- |
| byte | unsigned | 1바이트 | 정수 |
| sbyte | signed | 1바이트 | 정수 |
| short | signed | 2바이트 | 정수 |
| ushort | unsigned | 2바이트 | 정수 |
| int | signed | 4바이트 | 정수 |
| uint | unsigned | 4바이트 | 정수 |
| long | signed | 8바이트 | 정수 |
| ulong | unsigned | 8바이트 | 정수 |
| char |  | 2바이트 | 유니코드 문자 |
| float |  | 4바이트 | 소수 |
| double |  | 8바이트 | 소수 |

# 메서드 (함수)

> 함수의 선언, 정의
> 

```csharp
void Move(int hp, int distance)
{
	...
}

int GetRandomDistance()
{
	int ranNum;
	...
	return ranNum;
}
```

C언어와 거의 차이가 없다. 함수 이름의 앞에 반환할 데이터 타입, 함수 이름의 뒤에 입력 받을 데이터 타입과 변수명을 적는다. 그런데 C#에서는 매개변수가 비어있을 때 void로 선언할 수 없는 것 같다..

- [void를 썼을 때 발생하는 에러](https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs1536?f1url=%3FappId%3Droslyn%26k%3Dk(CS1536))

> 아래 함수 선언은 C#에서 불가능.
> 

```csharp
int GetRandomDistance(void);
```

# 조건문, 반복문

if, while, for문은 C언어와 형식 같음.

C언어에선 조건식에 숫자가 있다면 0은 거짓, 0이 아닌 숫자는 참으로 판별했는데, C#에선 조건식에 true, false 혹은 true, false로 반환되는 식만 사용 가능하다.

> 아래 코드는 C#에서 불가능.
> 

```csharp
if (1)
{
	...
}
```

# 연산자

비교연산자, 논리연산자 같음

# 배열

> 배열의 선언
> 

```csharp
int[] students = new int[5];
string[] stringArr = new string[3];
```

> 배열의 선언과 동시에 초기화
> 

```csharp
int[] students = new int[] {100, 90, 80, 70, 60};
string[] stringArr = new string[] {"one", "two", "three"};
```

배열의 인덱스 접근 방식은 C언어와 같다.

> C++에서 배열의 선언
> 

```csharp
int arr[5];
int *arr = new int[5];
```

위 코드는 각각 int형 배열을 스택과 힙에 할당한 것이다. C#코드에서 new 연산자를 사용한 걸 보니 힙 메모리에 할당한 것 같긴 한데.. C#에서의 스택, 힙 메모리 사용에 관해서도 이후에 알아봐야 할 것 같다.