using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject   gameoverText;
    public Text         timeText;
    public Text         recordText;
    public Text         exitText;

    private float       surviveTime;
    private bool        isGameover;

    void Start()
    {
        surviveTime = 0;
        isGameover = false; 
    }
    void Update()
    {
        if (!isGameover)
        {
            // 생존시간 갱신
            surviveTime += Time.deltaTime;
            // 갱신한 생존시간을 timeText Text 텍스트 컴포넌트를 이용해 표시
            timeText.text = "Time : " + (int) surviveTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
    public void EndGame()
    {
        // 현재 상태를 게임오버 상태로 전환
        isGameover = true;

        // 게임오버 텍스트 게임 오브젝트 활성화
        gameoverText.SetActive(true);

        // BestTime 키로 저장된 이전까지의 최고 기록 가져오기
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        // 이전까지의 최고 기록보다 현재 생존시간이 더 크다면
        if (surviveTime > bestTime)
        {
            // 갱신
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("Besttime", bestTime);
        }
        // 최고 기록 표시
        recordText.text = "Best Time: " + (int)bestTime;
    }
}
