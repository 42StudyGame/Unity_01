# 4week 

**블로그 포스팅 내용**

*정리는 블로그에 md형식으로 정리해서 해당 내용을 복붙하였습니다.*

# 게임 프로그래밍 패턴  

디자인 패턴, 프로그래밍 패턴등으로 불리며 많이 사용되는 `패턴`자레를 활용한다.  

효율성, 가독성, 공간등이 월등하게 좋아지는 경우도 있으며 상황에 맞게 해당 패턴을 구사할 수 있다면 좋은 결과가 나온다.  

코드를 엉망으로 짜다보니 이 책이 간절해졌다..

디자인 패턴 또는 프로그래밍 패턴은 중복되는 코드를 효율적인 해결책이다.  

많은 사람들이 말하길 '효율적인 코드를 만들기위한 방법론'정도로 설명한다.  

게임에 적용되는 디자인 패턴의 종류도 매우 다양하지만 그 활용도가 매우 높아서 필수적으로 활용해야 한다고 본다.  

앞으로 게임프로그래밍 패턴에 대해서 공부하며 포스팅할 예정인데, 해당 예제는 `C++`로 이루어져 있기 때문에 내가 예제를 C#으로 만들어 유니티로 테스트해볼 예정이다.  

*카테고리가 C#인 이유.. 직접 변형해가면서 필요한 상황을 익히려고 한다..*  

## 구조  

소프트웨어에서 구조란, 코드를 어떤식으로 구성하는지에 대한 말인 것 같다.  

내가 지금짜고 있는 구조가 좋은 구조인지 안좋은 구조인지는 짜는 본인이 알고있으며 조금 더 좋은 구조로 변경할 수 있음을 아는게 중요하다.  

또한, 내가 코드를 수정하고자 한다면 해당 코드의 구조를 파악해야 한다.  

> 이 과정은 아직도 많이 부족함을 알지만 다른 사람의 코드를 보고 해석하는 능력은 정말 반복의 연속이라는 생각이다.  

문제를 해결하기 위해서 코드를 추가한다고 해도 해당 코드가 다른 코드들과 영향이 없고 독립적으로 잘 돌아간다면 문제가 없지만, 코드한줄로 프로그램이 망가질 수 있기 때문에 **구조**피악하는게 중요하다.  

### 디커플링

간단하게 말해서 플레이어와 함정의 관계정도로 생각하면 이해가 잘된다.  

책에서는 양쪽 코드중에서 한쪽이 없다면 다른 한쪽 코드를 이해할 수 없는 것을 커플링  

이 코드를 디커플링하게 된다면 한쪽 코드만 있어도 해당 동작을 이해할 수 있다.  

---  

앞서 디커플링에 대해서 설명한 이유는 코드의 이해단계(구조파악)가 매우 줄어든다는 점이다.  

또한, 코드의 추가부분도 줄어들게 된다.  

한코드를 변경했다면 다른 코드도 연결지어서 변경해야 하지만, 커플링이 적은 코드라면 게임에 미치는 요소 또한 줄어들게 된다.  

이러한 요소 때문에 디커플링패턴이 많이 사용되며 요소가 되는 추상화, 모듈화, 디자인패턴, 소프트웨어 구조가 각광받는다.  

같은 기능을 하는 프로그램이라도 독립성이 보장된다면 이해와 수정이 정말 빠르게 이루어지기 때문에 생산성이 높아진다.  

하지만 그만큼 코드의 구조를 잡는 일은 매우 어렵다..  

많은 사람들이 확장하기 쉬운 코드베이스(ex. 완벽한 부모클래스..같은?)를 갈망하지만 이 부분자체가 `예측`이라는 항목이 들어가기 때문에 디버깅과 유지보수에 그만큼의 시간을 할애한다.  

확장성(일반화)에 심취하게 되면 인터페이스, 추상화, 가상메서드 등등 확장포인트가 늘어나게 되면서 구조 자체가 망가지게 될 수 있다.  

커플링 패턴을 피해서 디커플링을 추구하지만 망상 추상화계층에 먹히고 마는 것.. 

## 성능 그리고 속도  

혹자는 소프트웨어의 구조 그리고 추상화가 오히려 게임의 성능을 제한시킨다는 비판을 한다.  

코드 자체를 유연하게 하는 가상함수, 인터페이스, 포인터, 메시지등 같은 메커니즘에 의존하지만 다들 어느정도 런타임비용을 요구 한다.  

이 때문에 성능이나 속도를 고려한다면 `예측` 그리고 `가정`이 매우 중요하다.  

> 모든 개체가 같은 클래스라고 한다면 배열에 집어넣어서 깔금하게 사용하거나 한가지 자료형이 한가지 메서드만 호출한다면 정적으로 바인딩된 메서드를 호출하는 방법이 있다.  

이러한 견해나 사례로 유연성 자체가 나쁘다는 점을 피력하는 것이 아니라 구조자체를 만들 때 무조건적으로 좋다는 것이 아니라는 것을 말해준다.  

이러함을 알아도 유연성을 포기하지 못하는 이유는 개발속도 그리고 확장성 측면에서 비약적으로 좋기 때문이다.  

*ps. 책에서 추천하는 방법으로 처음에는 유연하게 코드를 유지하다. 기획이 확실해지면 추상계층을 제거해 성능을 높이는 방법이 있다.*

## 나쁜코드  

좋은 코드는 구현함에 있어 시간이 오래걸린다. 해당 코드에 대한 유지성, 확장성을 고려해야하기 때문.  

나쁜 코드는 지금 당장의 기능을 실행하기 위해서 만드는 코드를 예로 들 수 있다.  

프로토타입을 구현함에 있어, 기능적인 부분만 보여주기 위해서 만들었다고 한다면 유지할 수 있는 상태가 아니기 때문에 반드시 다시 만들어야한다.  

따라서 나쁜코드(버릴코드)를 작성하더라도 유지해야할 가능성을 봐야한다.  

## 균형 잡기  

게임을 완성하는데 있어서 가장 좋은 목표를 예로 든다면 아래와 같다.  

1. 프로젝트 개발 기간동안 코드를 쉽게 이해할 수 있도록 구조를 깔끔하게 만들고 싶다. 
2. 실행성능을 최적화 하고 싶다. 
3. 지금 개발 중인 기능을 최대한 빠르게 구현하고 싶다.  

목표는 매우 달콤해보이지만 어느정도 서로에게 상반된다.  

좋은 구조는 장기적으로 생산성을 높여주지만 구조를 좋게 유지할려면 코드를 변경할 때 마다 노력을 더욱 해야한다.  

최적화에는 매우 많은 시간이 소요되기 때문에 빠르게 구현한 결과물이 좋은 실행속도를 내는 경우는 드물다.  

또한 최적화가 매우 잘된 코드들은 유연하지 않아서 고치기가 어렵다..  

세가지 목표 각각의 장단점이 확실하기 때문에 우리는 적절한 균형을 잡아야 한다. 

## 단순함 

위의 문제를 조금이라도 쉽게 바라볼 수 있는 수단은 `단순함`이다.  

코드를 작성할 때 코드를 최대한 단순하고 간결하게, 문제를 해결하는 방향으로 짜게되면 코드만 읽어도 의도를 쉽게 파악할 수 있고 그외 다른 방법이 떠오르지 않는다.  

전체적인 코드의 양이 줄어들고 본인이 생각해야하는 양또한 줄어들게 된다.  

이러한 코드를 짜는 팁은 필요없는 코드를 빼는 것 부터 시작한다.  

## 마무리  

정리하며 읽는 내내 정말 내가하고 있는 고민들을 다들 하고 있다는 점에서 놀랐고, 내가 그동안 외면했던 문제들을 직면할 수 있어서 좋았다..!  

1. 추상화와 디커플링을 잘 활용하면 코드를 점차 쉽고 빠르게 만들 수 있다. 하지만, 지금 고민중인 코드에 유연함이 필요 없다면 디커플링과 추상화를 적용하느라고 시간낭비하지 말자..!  
2. 개발 내내 성능을 고민하고, 최적화에 맞게 설계해야한다. 하지만 가장 자체를 박아두는 저수준의 최적화는 가능하면 늦게해라  
3. 게임 기획에 맞추느라 코드를 엉망으로 만들지 말자, 그 코드로 작업하는 것은 본인이다.
4. **뭔가 재밌는 걸 만들고 싶다면 만드는 데에서 재미를 느껴라**

---

이 포스팅은 저자: **로버트 나이스트롬** 옮김: **박일**  

**게임 프로그래밍 패턴**책을 참고하였습니다.  

[링크](https://www.hanbit.co.kr/store/books/look.php?p_code=B4342659595)  


++ 08/15 추가 포스팅  

## SkeletonAnimationHandler  

위에서 다룬 다양한 제어방법을 한가지 컴포넌트로 만들어서 해당 메서드를 다양하게 활용할 수 있게 만들었다.  

spine starting에서 참고하였다.  

```cs
public class SkeletonAnimationHandler : MonoBehaviour
{
    public Spine.Animation TargetAnimation { get; private set; }

    private SkeletonAnimation _skeletonAnimation;

    [SerializeField] private List<StateNameToAnimationReference> _statesAndAnimation = new List<StateNameToAnimationReference>();
    [SerializeField] private List<AnimationTransition> _transitions = new List<AnimationTransition>();

    [System.Serializable]
    public class StateNameToAnimationReference
    {
        //public string stateName;
        //public @string animation;
        //public Spine.Animation animation;
        [SpineAnimation] public string stateName;
        public Spine.Animation animation;
    }

    [System.Serializable]
    public class AnimationTransition
    {
        [SpineAnimation] public string fromeName;
        public Spine.Animation from;
        [SpineAnimation] public string toName;
        public Spine.Animation to;
        [SpineAnimation] public string transitionName;
        public Spine.Animation transition;
    }

    private void Awake ()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();

        foreach (var entry in _statesAndAnimation)
        {
            
            SkeletonData skeletonData = _skeletonAnimation.skeletonDataAsset.GetSkeletonData(true);
            entry.animation = skeletonData != null ? skeletonData.FindAnimation(entry.stateName) : null;
            //this.animation = skeletonData != null ? skeletonData.FindAnimation(animationName) : null;
        }

        foreach (var entry in _transitions)
        {
            SkeletonData skeletonData = _skeletonAnimation.skeletonDataAsset.GetSkeletonData(true);
            
            entry.from = skeletonData != null ? skeletonData.FindAnimation(entry.fromeName) : null;
            entry.to = skeletonData != null ? skeletonData.FindAnimation(entry.toName) : null;
            entry.transition = skeletonData != null ? skeletonData.FindAnimation(entry.transitionName) : null;
        }
    }

    /// <summary>
    /// 2D 뒤집기 메서드
    /// </summary>
    /// <param name="horizontal"></param>
    public void SetFlip(float horizontal)
    {
        if (horizontal != 0)
        {
            _skeletonAnimation.skeleton.ScaleX = horizontal > 0 ? 1f : -1f;
        }
    }

    public void PlayAnimationForState(string stateShortName, int layerIndex)
    {
        PlayAnimationForState(StringToHash(stateShortName), layerIndex);
    }

    /// <summary>
    /// PlayAnimationForState Overloading 해당 애니메이션을 실행
    /// </summary>
    /// <param name="stateShortName">실행하고자 하는 애니메이션 이름</param>
    /// <param name="layerIndex">트랙/레이어 번호</param>
    public void PlayAnimationForState(int stateShortName, int layerIndex)
    {
        var foundAnimation = GetAnimationForState(stateShortName);
        if (foundAnimation == null)
            return;

        PlayNewAnimation(foundAnimation, layerIndex);
    }

    public Spine.Animation GetAnimationForState(string stateShortName)
    {
        return GetAnimationForState(StringToHash(stateShortName));
    }
    
    /// <summary>
    /// GetAnimationForState Overloading 해당 애니메이션을 반환(없다면 null)
    /// </summary>
    /// <param name="stateShortName">찾고자 하는 애니메이션 이름(정수로 들어옴)</param>
    /// <returns>해당 애니메이션</returns>
    public Spine.Animation GetAnimationForState(int stateShortName)
    {
        var foundState = _statesAndAnimation.Find(entry => StringToHash(entry.stateName) == stateShortName);
        return ((foundState == null) ? null : foundState.animation);
    }

    /// <summary>
    /// 애니메이션 재생 메서드
    /// 현재 진행중인 애니메이션이 없다면 || 전환 애니메이션이 없다면 바로 애니메이션 전환
    /// 있다면 전환 애니메이션 우선 재생 후 재생
    /// </summary>
    /// <param name="target"></param>
    /// <param name="layerIndex"></param>
    public void PlayNewAnimation(Spine.Animation target, int layerIndex)
    {
        Spine.Animation transition = null;
        Spine.Animation current = null;

        current = GetCurrentAnimation(layerIndex);
        if (current != null)
            transition = TryGetTransition(current, target);

        if (transition != null)
        {
            _skeletonAnimation.AnimationState.SetAnimation(layerIndex, transition, false);
            _skeletonAnimation.AnimationState.AddAnimation(layerIndex, target, true, 0f);
        }
        else
        {
            _skeletonAnimation.AnimationState.SetAnimation(layerIndex, target, true);
        }

        this.TargetAnimation = target;
    }

    /// <summary>
    /// OneShot 애니메이션 메서드
    /// </summary>
    /// <param name="oneShot">한번 재생할 애니메이션 클립</param>
    /// <param name="layerIndex">null</param>
    public void PlayOneShot(Spine.Animation oneShot, int layerIndex)
    {
        var state = _skeletonAnimation.AnimationState;
        state.SetAnimation(0, oneShot, false);

        var transition = TryGetTransition(oneShot, TargetAnimation);
        if (transition != null)
        {
            state.AddAnimation(0, transition, false, 0f);
        }
        
        // delay fix..!
        state.AddAnimation(0, TargetAnimation, true, oneShot.Duration);
    }

    /// <summary>
    /// 현재 애니메이션에서 다음 애니메이션으로 전환될 때 전환 애니메이션이 있는지 판단
    /// </summary>
    /// <param name="from">현재 애니메이션</param>
    /// <param name="to">다음 애니메이션</param>
    /// <returns>없다면 null 있다면 전환애니메이션(ex)ldel-to-jump)</returns>
    private Spine.Animation TryGetTransition(Spine.Animation from, Spine.Animation to)
    {
        foreach (var transition in _transitions)
        {
            if (transition.from == from && transition.to == to)
            {
                return transition.transition;
            }
        }

        return null;
    }

    /// <summary>
    /// 현재 애니메이션을 불러오는 메서드
    /// </summary>
    /// <param name="layout">해당 애니메이션 트랙/layout</param>
    /// <returns>Spine.Animation</returns>
    private Spine.Animation GetCurrentAnimation(int layout)
    {
        var currentTrackEntry = _skeletonAnimation.AnimationState.GetCurrent(layout);
        return (currentTrackEntry != null) ? currentTrackEntry.Animation : null;
    }

    /// <summary>
    /// 애니메이션 문자열을 해쉬값으로 반환
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private int StringToHash(string str)
    {
        return Animator.StringToHash(str);
    }
    
}
```

위 처럼 cs를 만들어 놓으면 unity animation과 같이 duration, delay, oneshot등 다양한 기능들을 비슷하게 사용할 수 있다.  

spine은 mix의 기능을 사용할 수 있기 때문에 함수를 하나 추가하여 mix기능의 함수를 넣어도 되고 다양하게 활용이 가능하다.  

*왜 기능들을 직접 만들어 봐야하는지 이해가 잘된 예제*  

1. 애니메이션 플레이함수 `PlayNewAnimation`
   1. 함수에 진입하여 연결되는 애니메이션인지 체크하고 아니라면 해당 애니메이션을 set으로 강제 재생(해당 트랙으로) 
   2. 주로 이동관련 애니메이션?
2. 애니메이션 oneshot함수 `PlayOneShot`
   1. 우선순위를 무시하고 해당 0번 트랙에 set으로 호출 후 prev 즉, 전애니메이션 add로 뒤로 밀어준다. 
   2. 공격이나 피격등 any state에 해당된다.  

나머지는 해당 함수에서 사용하는 유틸함수들이다.  

생각보다 훨씬 spine내부에 사용가능한 파리미터나 함수가 많아서 다양한 연출이 가능하다.  

조금 더 뜯어봐야겠다..!
