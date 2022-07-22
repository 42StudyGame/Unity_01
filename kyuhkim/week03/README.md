# 3주차: 3부 닷지 플레이어 제작 (220p - 359p)

    ## 6장 닷지: 플레이어 제작
    ## 7장 닷지: 탄알 제작
    ## 8장: 닷지: 게임 매니저와 UI, 최종 완성
    ## 과제
        1. 3부 닷지 게임의 제작 예제 프로젝트를 github에 올리고 플레이 영상도 올려보기
        2. 만약 xbox controller나 다른 컨트롤러가 있는 분은 연결해서 해보세요 재밌습니다!

---
# 노트
---

text 대신 textMeshPro를 이용해봄
textMesh의 경우에는 vector방식을 이용하므로, 폰트 확대 축소를 해도 깨지지 않음.
글자에 그래픽 효과를 부여하기 좋음.

[텍스트 메시 장점](https://mumumi.tistory.com/141)

esc키 두번 누르면, 종료하게 변경
(중간에 다른키가 섞이면 취소되지만, 두번 누르는 행위에 시간 제한을 두지는 않았음. 따라서 esc누르고 100만년동안 아무런 버튼도 안누르다가 esc 눌러도 2회의 esc 입력으로 간주되어 종료됨)



의문: 객체들을 평면적 구조에 놓는게 좋은 것인가? (문득 그런 생각이 들긴 하는데, 실제로도 그런가?)

bullet: 총알의 라이프사이클, 충돌이벤트 처리 (playerController의 Die message 호출)
bulletSpawner: 대상을 정하고(FindObjectOfType), 일정 시간마다 대상을 향하는 bullet생성
gameManager: UI와 게임 플레이 사이클을 관리 
playerController: 게임메니저를 찾고(FindObjectOfType) 플레이어의 이동, 사망 처리 (이때 gameManager의 Endgame message 호출)
rotator: level을 돌리는 역할만 함 (독립적)

bullet과 bulletManager(Spawner) 관계 이외에는 각 객체들이 평면적으로 놓여있다는 생각이 조금 든다.
FindObjectOfType로 검색하는 행동이 있으나, drag&drop으로 옮기지 않고 저렇게 검색을 했다는건... 객체간의 상호 존중? 이라는 개념이 있어서일까?

text같은 경우에는, drag&drop으로 처리하기는 했으나... playerController가 UImanager역할도 겸하고 있어서 bullet, bulletSpawner관계처럼 처리한것 같아 보이고...

-각 객체와 그걸 관리할 책임이 있는 관리자가 아니면, 서로간에는 평면적(대등하게?) 놓고 취급하고 있다-
라는 가설을 세우고, 앞으로의 과정을 살펴봐야겠다.
