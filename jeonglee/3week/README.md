# 3week 

**블로그 포스팅 내용**

*정리는 블로그에 md형식으로 정리해서 해당 내용을 복붙하였습니다.*

# 유니티 스파인 적용 및 사용법 

Spine이란, 2D를 사용하는 프로젝트에서 정말 많이 사용되는 툴로 쉬운 제작, 높은 퀄리티 및 생산성을 가진다.  

스파인에 대한 장점은 해당 사이트에서 쉽게 정리된 글로 확인할 수 있다. 

[<mark style='background-color: #A9BCF5'>스파인 장점</mark>](http://ko.esotericsoftware.com/spine-in-depth)

전 프로젝트에서는 스프라이트 애니메이션으로 제작을 했지만 이번 프로젝트는 스파인을 사용하기로 했다..!  

## Runtime Spine Import  

스파인 캐릭터를 유니티에서 원활하게 사용하기 위해선 스파인 공식홈페이지에서 프로그램을 다운받아야 한다.  

[<mark style='background-color: #A9BCF5'>다운로드 링크</mark>](http://ko.esotericsoftware.com/spine-unity-download)

맨위의 **spine-unity(unity package)**를 받아주면 된다. (패키지로 주기 때문에 설치가 간단하다.)

아래 URP(Universal Render Pipeline)까지 받고 싶다면 클릭해서 설치하면 된다.  

package를 import하게 되면 spine에 관련된 패키지와 Spine Examples가 다운이 된다.  

*Spine Examples를 뜯어보면서 공부하는게 가장 효율이 좋았다.*  

## spine에서 유니티

아트파트나 본인이 스파인에서 작업한 결과물을 유니티로 옮기고 싶다면 spine을 유니티 export형식에 맞춰서 빼준다음 unity로 옮기게 되면 자동으로 연동이 된다.  

*atlas.txt, json, png총 3가지 확장자 파일을 옮기면 된다.*  

**name_SkeletonData**파일로 만들어지게 된다.  

해당 파일을 씬으로 옮기면 실제로 동작하는 모습을 확인할 수 있고 인스펙터 창에서 애니메이션을 바꿔가며 확인할 수 있다.  

![이미지](./../../../../assets/images/Unity_img/spine/spine_skeletonData.png)  

> Animation Name 필드를 선택하면 된다.

## 유니티에서 애니메이션 제어 (1) 

해당 캐릭터를 게임과 같이 동작시킬려면 애니메이션 동작에 관한 처리를 코드르 작성해야하는데 이는 unity Aniamtor컴포넌트를 사용해서 다루는게 아니다.  

*SkeletonAnimation*컴포넌트를 통해서 관리할 수 있다.  

따라서 코드에서 제어하기 위해서는 해당 컴포넌트 레퍼런스에 접근하여 state에 내장된 함수로 제어한다.  

![이미지](./../../../../assets/images/Unity_img/spine/spine_lifeCycle.png)

> 실제 spine Examples 예제 중 하나

```cs
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples {
	public class SpineBlinkPlayer : MonoBehaviour {
		const int BlinkTrack = 1; // 해당 애니메이션 재생 트랙

		public AnimationReferenceAsset blinkAnimation; // 재생할 애니메이션 클립
		public float minimumDelay = 0.15f;
		public float maximumDelay = 3f;

		IEnumerator Start () {
			var skeletonAnimation = GetComponent<SkeletonAnimation>(); if (skeletonAnimation == null) yield break;  
      // 컴포넌트 할당 그리고 예외처리(방어적 프로그래밍 필수)
			while (true) {
				skeletonAnimation.AnimationState.SetAnimation(SpineBlinkPlayer.BlinkTrack, blinkAnimation, false);
        // 해당 애니메이션 재생 순서대로 트랙, 재생할 애니메이션 클립, 반복여부

				yield return new WaitForSeconds(Random.Range(minimumDelay, maximumDelay));
			}
		}

	}
}
```

위 처럼 사용할 수 있다 내부 기능도 매우 다양하고 트랙을 설정할 수 있어서 애니메이션을 섞어서 사용가능하다.  

해당 예제를 확인할려면 Spine Examples -> Getting Started -> The Spine GameObject이다.  

스파인의 정말 좋은 기능은 동시에 애니메이션을 재생할 수 있다는 점이다.  

위의 예제를 살짝 변형해서 Idle 동작을 합칠 수 있다.  

```cs
namespace Spine.Unity.Examples {
	public class SpineBlinkPlayer : MonoBehaviour {
		const int BlinkTrack = 1;

		public AnimationReferenceAsset blinkAnimation;
		public AnimationReferenceAsset IdleAnimation;  // Idle 애니메이션 추가
		public float minimumDelay = 0.15f;
		public float maximumDelay = 3f;

		IEnumerator Start () {
			var skeletonAnimation = GetComponent<SkeletonAnimation>(); if (skeletonAnimation == null) yield break;

			skeletonAnimation.AnimationState.SetAnimation(0, IdleAnimation, true); // 0번 트랙에 루프로 재생
			while (true) {
				skeletonAnimation.AnimationState.SetAnimation(1, blinkAnimation, false);
				yield return new WaitForSeconds(Random.Range(minimumDelay, maximumDelay));
			}
		}
	}
}
```

![이미지](./../../../../assets/images/Unity_img/spine/spine_ani.gif)  

---  

기본적인 동작방식을 정리해보자  

1. AnimationReferenceAsset형식을 통해서 애니메이션클립에 접근할 수 있다.  
2. 애니메이션제어를 하기 위해서 SkeletonAnimation컴포넌트에 접근해야 한다.  
3. AnimationState는 현재 애니메이션 설정을 할 수 있다.  

보기 처럼 스파인에 대한 정보가 그렇게 많지 않기 때문에 스파인에서 제공한 Example을 보고 학습하는게 도움이 된다.  

## 유니티에서 애니메이션 제어 (2)  

앞에서 AnimationReferenceAsset형식을 통해 애니메이션을 에디터상에서 드래그로 적용할 수 있다고 했지만 조금 더 유용한 방법이 있다.  

애니메이션이 100개가 넘어간다고 했을 때 전부 드래그나 직접 클릭하여 하나씩 넣기에는 무리가 있다.  

spine에서 제공하는 애트리뷰트로 **[SpineAnimation]**이다.  

```cs
[SpineAnimation]
public string runAnimationName;
```

위 형식으로 스크립트를 등록해두면 해당 모델에 등록되어 있는 애니메이션을 선택할 수 있도록 드롭다운 메뉴로 제공된다.  

![이미지](./../../../../assets/images/Unity_img/spine/spine_spineAnimation.png)  

앞에서 애니메이션을 재생하고 싶을 때 **skeletonAnimation.AnimationState.SetAnimation**함수를 사용했는데 AnimationState또한 클래스이기 때문에 변수에 연결하여 사용이 가능하다.  

또한 해당 모델에 접근하기 위해선 **skeleton**에 접근해야 한다.  

```cs
// Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
public Spine.AnimationState spineAnimationState;
public Spine.Skeleton skeleton;

void Start () {
  skeletonAnimation = GetComponent<SkeletonAnimation>();
  spineAnimationState = skeletonAnimation.AnimationState;
  skeleton = skeletonAnimation.Skeleton;
}
```

접근 방식은 위 처럼 변수에 담아두고 사용하면 되지만 serialized objects오브젝트가 아니기 때문에 인스펙터창에 뜨지 않는다..! 

*SerializeField를 선언해도 뜨지 않는다.*  

해당 클래스나 메서드 등에 대해 자세하게 알고 싶다면 spine 공식문서를 참고..!  

[<mark style='background-color: #A9BCF5'>스파인 API 공식 문서</mark>](http://ko.esotericsoftware.com/spine-api-reference#Skeleton)  
[<mark style='background-color: #A9BCF5'>스파인 런타임 문서</mark>](http://ko.esotericsoftware.com/spine-unity#spine-unity)


## 유니티에서 애니메이션 제어 (3)  

스파인의 장점 중 하나인? event제어이다.  

스파인 제작 시 특정한 애니메이션에 이벤트를 미리 걸어 둘 수 있고 프로그래머는 해당 이벤트를 호출 받아서 동작할 수 있다.  

쉽게 말해서 걷는 애니메이션에서 땅에 발이 닫는 순간 소리를 재생하게 하는 이벤트를 이름만 걸어두면 프로그래머는 해당 이벤트를 이용하여 제작이 가능하다.  

이러한 기능을 **spineEvent**라고 한다. 

*사용하기 간편한 attribute를 사용*

```cs
[SpineEvent]
public string eventName;
```

마찬가지로 인스펙터 창에서 드롭다운으로 해당 애니메이션 이벤트를 선택할 수 있다.

스파인 이벤트는 보라색 말풍선 모양이니 기억할 것

미리 아트파트와 이름을 맞추는게 좋아보인다..!  

![이미지](./../../../../assets/images/Unity_img/spine/spine_spineevent.png)

이름만 연결 시켜 놓는 것이기 때문에 실제 레퍼런스를 할당해야 하는데 이러한 기능까지 미리 메서드로 구현되어 있다.  

```cs
EventData eventData;
// 이벤트 데이터 클래스(내부는 가볍다)

eventData = skeletonAnimation.Skeleton.Data.FindEvent(eventName);
// 해당 문자열과 해당 모델에 있는 이벤트를 foreach문으로 탐색한다.  
```

이제 해당 이벤트를 연결까지 했으니 특정 이벤트에만 발생하게 만들면 된다.  

skeletonAnimation의 클래스 프로퍼티 AnimationState의 이벤트 메서드 event를 사용하여 메서드를 등록하고 해당 메서드의 파라미터로 주어진 이벤트 값과 해당 이벤트를 비교하여 제어하면 된다..  

```cs
// 이벤트 메서드로 등록
skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;

// 이벤트가 발생할 때 마다 호출
private void HandleAnimationStateEvent (TrackEntry trackEntry, Event e) {
  bool eventMatch = (eventData == e.Data); 
  if (eventMatch) {
    Play();
  }
}
```

같은 맥락으로 애니메이션이 실행될 때 종료될 때 호출되는 콜백 메서드인 start, end, complete가 있다.

*complete는 loop의 경우 애니메이션 종료 시*

![이미지](./../../../../assets/images/Unity_img/spine/spine_callback.png)

자세한 건 아래 링크를 통해서 보는게 좋다..!

[<mark style='background-color: #A9BCF5'>spine event 공식 문서</mark>](http://ko.esotericsoftware.com/spine-unity-events)

# Cinemachine?  

시네머신은 카메라 로직을 개발할 필요가 없이 손 쉽게 카메라를 조작가능한 유니티 내장 패키지이다.  

*사용하기 위해선 패키지 매니저에서 시네머신을 다운 받아야 한다.* 

[<mark style='background-color: #A9BCF5'>시네머신 소개</mark>](https://unity.com/kr/unity/features/editor/art-and-design/cinemachine)  
[<mark style='background-color: #A9BCF5'>시네머신 메뉴얼</mark>](https://docs.unity3d.com/Packages/com.unity.cinemachine@2.2/manual/index.html)   

* 여기서 제공하는 정보 및 튜토리얼도 매우 유용하다..!  

기본적으로 시네머신은 카메라 오브젝트를 생성하지 않으며 vrtual camera(가상 카메라)를 생성한다.  

가상 카메라는 원하는 postion에서 동적으로 씬을 보여주기 때문에 다양한 연출이 가능해진다.  

가상 카메라끼리는 영향을 미치지 않기 때문에 자유로운 구성이 가능하다.  

또한, 비용측면으로도 시네머신이 기본카메라 보다 프로세스를 적게 잡아 먹기 때문에 유용하게 활용할 수 있다.  

## 시네머신의 기본 

시네머신의 기능을 이해하기 앞서 Cinemachine Brain을 이해하고 넘어가야 한다.  

Cinemachine Brain은 하이어라키에서 가상카메라 생성 시 자동으로 기본카메라에 부착되는 컴포넌트이다.  

![이미지](./../../../../assets/images/Unity_img/Cinemachine/unity_chnemachine.png)  

씬에 존재하는 모든 가상카메라를 관리감독하는 컴포넌트로 가상카메라의 `뇌`이다.  

하이어라키에서 cinemachine을 눌르게 되면 많은 카메라들이 나오게 되는데... 

1. Virtual Camera: 말 그대로 가상 카메라 이며 자유롭게 배치할 수 있다. 
2. FreeLook Camera: 타켓을 중심으로 원을 생성하여 해당 구간 찍는다.
3. Blend List Camera: 가상 카메라들의 정해진 방식대로 순차적으로 전환되는 카메라
4. **State-Driven Camera**: 애니메이션의 상태별로 전혼되는 카메라
5. ClearShot Camera: 충돌상태에 따라 전환되는 카메라
6. Dolly Camera: 트랙으로 움직이는 카메라
7. Target Group Camera: 그룹으로 계산된 화면을 보여주는 카메라
8. Mixing Camera: 
9. 2D Camera: 직교?로 사용되는 카메라  

*종류가 매우 많네..?* 

## 가상 카메라

![이미지](./../../../../assets/images/Unity_img/Cinemachine/unity_chnemachine_1.png)  

Status: 에디터에서 해당 카메라를 선택할 수 있다.(조정할때 사용)  
Game Window Guides: 가이드라인을 보여준다.(마우스로 조작 가능)
Save During Play : 유니티상에서 플레이 중에 변경한 정보를 저장    
Property : 카메라의 우선순위  
Follow : 어떤 오브젝트를 따라다닐지 설정한다.  
Look At : 어떤 오브젝트를 바라볼지 설정한다.  
Standby Update : Live 상태가 아닌 카메라의 업데이트 빈도 설정.  
(Never - 항상 / Always - Live일때만 / Round Robine - 정기적으로)  
Lens : 카메라 렌즈를 설정.  
Transitions : 카메라 사이를 이동할 때 씬 전환 효과 설정.  
Body : 씬 내부의 버추얼 카메라가 움직일 때 따라가는 알고리즘 설정을 변경한다.  
Aim : 씬 내부의 버추얼 카메라가 Look At 타깃을 바라볼 때의 따라가는 알고리즘 설정을 변경한다.  
Extensions: 정말 다양한 추가적인 기능을 설정할 수 있다..


## 시네머신 사용법

앞 포스팅이 스파인 포스팅이였는데 예제파일들을 뜯어보며 공부했다.  

시네머신도 그런 방법이 있는지 알아보다 Package Manager에서 처음 import할 때 아래 Sample토글에서 받을 수 있는걸 알았다..!  

*뜯어보면서 삽질해보는게 가장 좋다..!*  

유니티 공식 유튜브에서 친철하게 한국어로 알려주는 강의도 있다.  

[<mark style='background-color: #A9BCF5'>시네머신 파헤치기</mark>](https://www.youtube.com/watch?v=2oOIp22Y11U)  

두시간이 아깝지 않다..!

> 만약 시네머신에 기능적인 부분을 수정하고 싶다면 직접 소스코드를 수정하여 기능을 추가할 수 있다.  

현재 필요한 예제들을 먼저 뜯어보고 필요할 때 마다 포스팅을 이어가겠다.

### FollowCam

> Cinemachine Example Scenes -> Scenes -> FollowCam

![이미지](./../../../../assets/images/Unity_img/Cinemachine/cinemachine_2.gif)  

가장 기본적이며 보편적으로 많이 사용하는 기능이다.

코드로 설정하지 않고 시네머신에디터상에서 타켓을 지정하여 가상카메라가 따라다니며 비춰주는 방식이다.  

해당 씬을 시작해보면 날아다니는 비행기를 카메라가 부드럽게 따라다닌다.  

![이미지](./../../../../assets/images/Unity_img/Cinemachine/unity_chnemachine_2.png)  

vcam을 보면 다음과 같이 follow즉 따라다닐 대상, look at 바라보는 대상이 동일함을 알 수 있다.  

매우 부드럽게 움직이는 이유는 아래 body의 항목들의 설정때문이다.  

가장 쉽게 이해하는 방법은 실행 후 인스펙터창을 통해 값을 조작해보는 것이다.  

### CameraMagnets  

> Cinemachine Example Scenes -> Scenes -> CameraMagnets

![이미지](./../../../../assets/images/Unity_img/Cinemachine/cinemachine_1.gif)  

이번 씬은 현재 2D 플랫포머에서 아이템을 발견하거나 가까이 갔을 때 카메라가 해당 오브젝트쪽을 밝혀주는 방식이다.  

#### Cinemachine Confiner  

가장 먼저 맵에서 보이는 특징은 카메라가 지정된 맵사이즈 이상을 나가지 않는다는 것이다.  

이는 게임에서 많이 사용되는 방식으로 2D의 경우 카메라가 벽을 뚫고 나가게 되면 플레이어는 뒤에 무언가 있거나 몰입도가 떨어질 수 있다.  

전에는 코드로 스크린 사이즈와 너비와 높이를 계산하여 나가지 못하게 하는 방식으로 동작했어는데 이 기능을 알고 나니 충격적이다..!  
*물론 편리한 기능을 코드로 작성해보는 건 매우 좋은 경험이다..*  

사용방법은 간단하다.  

1. CinemachineVirtualCamera컴포넌트의 Extensions에서 Cinemachine Confiner추가한다.  
2. 바로 아래 Cinemachine Confiner가 생기게 되는데 Confiner2D로 설정하고 아래 해당 영역을 지정하는 Collider를 두면 된다.  
3. confine screen Edges체크하고 실행하면 해당 Collider영역에서만 카메라가 이동한다.  

*Collider는 Polygon, composite만 가능*

polygon의 경우는 맵형태에 맞게 설계가 가능하기 때문에 휠씬 범용성이 좋고 에디터로 수정할 수 있기 때문에 맵디자이너가 만지기도 편함.  

#### CinemachineTargetGroup  

이 씬은 CinemachineTargetGroup컴포넌트를 활용한 것인데 해당 컴포넌트는 vcam의 follow로 등록되면 해당 그룹에 있는 오브젝트를 전부 사각형안에 보여준다.  

이를 활용하여 거리를 계산하고 weight값을 코드로 조정하여 부드럽게 카메라가 자석처럼 이끌리는 연출을 한것!  

```cs
using UnityEngine;

[ExecuteInEditMode]
public class CameraMagnetProperty : MonoBehaviour
{
    [Range(0.1f, 50.0f)]
    public float MagnetStrength = 5.0f;
    
    [Range(0.1f, 50.0f)]
    public float Proximity = 5.0f;

    public Transform ProximityVisualization;

    [HideInInspector] public Transform myTransform;

    void Start()
    {
        myTransform = transform;
    }
    void Update()
    {
        if (ProximityVisualization != null)
            ProximityVisualization.localScale = new Vector3(Proximity * 2.0f, Proximity * 2.0f, 1);
    }
}
```

자석 오브젝트에 적용한다.  

```cs
using UnityEngine;

public class CameraMagnetTargetController : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;

    private int playerIndex;
    private CameraMagnetProperty[] cameraMagnets;
    // Start is called before the first frame update
    void Start()
    {
        cameraMagnets = GetComponentsInChildren<CameraMagnetProperty>();
        playerIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < targetGroup.m_Targets.Length; ++i)
        {
            float distance = (targetGroup.m_Targets[playerIndex].target.position - 
                              targetGroup.m_Targets[i].target.position).magnitude;
            if (distance < cameraMagnets[i-1].Proximity)
            {
                targetGroup.m_Targets[i].weight = cameraMagnets[i-1].MagnetStrength * 
                                                  (1 - (distance / cameraMagnets[i-1].Proximity));
            }
            else
            {
                targetGroup.m_Targets[i].weight = 0;
            }
        }
    }
}
```

해당 오브젝트를 묶어는 주는 부모 오브젝트에 부착하여 사용한다.  

CinemachineTargetGroup의 좋은 예제는 BossCamera가 잘 나와있다..!  

> Cinemachine Example Scenes -> Scenes -> BossCamera
  
