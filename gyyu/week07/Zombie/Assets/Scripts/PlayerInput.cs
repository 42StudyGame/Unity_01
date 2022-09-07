using UnityEngine;

// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
// 감지된 입력값을 다른 컴포넌트들이 사용할 수 있도록 제공
public class PlayerInput : MonoBehaviour {
    public string VerticalMoveAxisName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
    public string HorizontalMoveAxisName = "Horizontal"; // 앞뒤 움직임을 위한 입력축 이름
    public string rotateAxisName = "Mouse X"; // 좌우 회전을 위한 입력축 이름
    public string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    public string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름
	
    public float verticalMove { get; private set; }
    public float horizontalMove { get; private set; }
    public float rotate { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }
    public bool topView { get; private set; }
    public bool firstView { get; private set; }
    public bool aimView { get; private set; }

    // 매프레임 사용자 입력을 감지
    private void Update() {
        // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
			verticalMove = 0;
			horizontalMove = 0;
            rotate = 0;
            fire = false;
            reload = false;
            topView = false;
            firstView = false;
            aimView = false;
            return;
        }
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		verticalMove = Input.GetAxis(VerticalMoveAxisName);
		horizontalMove = Input.GetAxis(HorizontalMoveAxisName);
		rotate = Input.GetAxis("Mouse X");
        fire = Input.GetButton(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);
		topView = Input.GetKeyDown(KeyCode.Alpha1);
		firstView = Input.GetKeyDown(KeyCode.Alpha2);
		aimView = Input.GetMouseButtonDown(1);
	}
}