# 9주차: 부록: 어드레서블 시스템 (974p - 1046p)

---
# 노트
---
어드레서블 시스템
- assetbundle을 관리하는 시스템
- 상대경로 설정 문법을 통해, 에셋의 위치에 관계없이 리소스를 제공하는 시스템으로 보임
- Build / Load Path 설정하는 문법이 있음
    - []: editor-time, build-time에 fix됨
    - {}: run-time에 사용함
    - 대괄호나 중괄호 안에 들어가는 값은, pair로 구성되서, 적어넣는것은 key값.
    - key를 이용해서 필요한 상황(editor, build, run)에 value값 가져다 씀
    - build는 addressable build를 뜻하는 것으로 보임
    
- zombie에서, item들을 어드레서블 시스템으로 변경해보자.
    - 우선 Resources폴더가 아니게 되므로, PhotonNetwork.Instantiate를 사용할 수 없으니 객체 관리 pool을 만들어주고, 수동으로 인스턴스를 생성할 수 있는 로직을 추가해야함 [참고](https://doc.photonengine.com/ko-kr/pun/current/gameplay/instantiation) 
    - .. RaiseEvent를 ItemSpawner에 사용해봤고, 만족스럽지는 않음.
```
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others
        };
        var sendOptions = new SendOptions
        {
            Reliability = true
        };
        
        PhotonNetwork.RaiseEvent(CustomEventCode.RequestEvent, param, raiseEventOptions, sendOptions);

```
    - 위 코드에, `CachingOption = EventCaching.AddToRoomCacheGlobal`를 추가해야하지만, remove할 방법을 따로 찾아야 함
    - `CachingOption = EventCaching.RemoveFromRoomCache` 만 하는 것으로는 불가능할 것 같고

- 



---
# 삽질
---


---
# 감상
---

