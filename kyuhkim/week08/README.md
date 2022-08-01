# 8주차: 7부 네트워크 협동 게임: 좀비 서바이버 멀티플레이어 (816p - 933p)

---
# 노트
---
포톤서비스 이용해서 멀티플 만들어보기.
클라이언트를 신뢰하면 안되는 이유에 대해서 설명해놓음.

/// <summary>
/// helpful message
/// </summary>
IDE를 사용하다보면, 함수 선택할때 도움말 메시지를 볼수있다.
이는, 위처럼 적어둔 것이 보이는것.
[자세한 내용](https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/xmldoc/recommended-tags)은 여기서 찾아보자 :)
협업에 필요하지만, 더 중요한건.. 멋있어보인다! ㅇㅅㅇ)/

- camera view 변환
fps모드와 top-down모드를 변환하도록 변경
priority를 변경함으로써 스위칭 되도록 함

[PunPRC]로 감싼 부분에서, 클라이언트 버전만 동일하게 하고 코드를 수정해버리면;; 얼마든지 crack가능해보인다. 이건 어쩔수 없는건가? 아니면 다른 방법이 있는걸까? speed hack같은것도, 충분히 가능할 것 같은디?
아니구나.. 이 프로젝트를 해킹할 이유가 없다. 이정도면 잘 된 보안이다.

사망페널티 -2000점: 멀티플레이할때 점수 하락이 없으면 긴장감 떨어짐


---
# 삽질
---
카메라 전환 속도는, brain에 있었다.
week08을 week07에 작성 해버렸다;; (아쉽게도, git을 올바로 배울 기회로 이어지질 못했다..)

FPS cam의 bind mode 를 world space로 해놓고.. 왜 안되지??
(lock to target with world up으로 해야함)

네트워크 코드에 파라미터값을 잘못 넣었다.


---
# 감상
---

