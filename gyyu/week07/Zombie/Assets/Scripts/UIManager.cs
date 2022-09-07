using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour {
	
	private static UIManager m_instance;
	public static UIManager instance
	{
		get
		{
			if (m_instance == null)
				m_instance = FindObjectOfType<UIManager>();
			return m_instance;
		}
	}

	public Text ammoText; // 탄알 표시용 텍스트
	public Text scoreText; // 점수 표시용 텍스트
	public Text timeText; // 생존 시간 표시용 텍스트
	private float surviveTime;		// 생존 시간
	public Text waveText; // 적 웨이브 표시용 텍스트
	public GameObject gameoverUI; // 게임오버 시 활성화할 UI

	public void UpdateAmmoText(int magAmmo, int remainAmmo) {
		ammoText.text = magAmmo + "/" + remainAmmo;
	}

	public void UpdateScoreText(int newScore) {
		scoreText.text = "Score : " + newScore;
	}

	public void UpdateTimeText() {
		surviveTime += Time.deltaTime;
		timeText.text = "Time : " + (int) surviveTime;
	}
	public void UpdateWaveText(int waves, int count) {
		waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
	}

	public void SetActiveGameoverUI(bool active) {
		gameoverUI.SetActive(active);
	}

	public void GameRestart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}