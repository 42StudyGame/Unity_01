using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : MonoBehaviour {
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameover = false; // 게임 오버 상태
    public Text ScoreText; // 점수를 출력할 UI 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트
    private int score = 0; // 게임 점수

    void Awake() {
        // 싱글톤 변수 instance가 비어있는가? 맨처음 들어오는 오브젝트만 할당된다.
        if (instance == null)
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        if (isGameover && (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int newScore) {
        if (!isGameover)
        {
            score += newScore;
            ScoreText.text = "Score : " + score;
        }
    }
    public void AddScore() {
        if (!isGameover)
        {
            score += 1;
            ScoreText.text = "Score : " + score;
        }
    }

    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }
}