using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;				// UI 관련 라이브러리
using UnityEngine.SceneManagement;	// 씬 관리 관련 라이브러리

public class GameManager : MonoBehaviour
{
	public GameObject	gameoverText;		// 게임오버시 활성화할 텍스트 게임 오브젝트
	public Text			timeText;			// 생존 시간을 표시할 텍스트 컴포넌트
	public Text			recordText;			// 최고 기록을 표시할 텍스트 컴포넌트
	private float		_surviveTime;		// 생존 시간
	private bool		_isGameover;			// 게임오버 상태
	void Start()
	{
		// 처음 생존시간과 게임 오버 상태를 초기화한다.
		_surviveTime = 0;
		_isGameover = false;
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{ 
			Application.Quit();
		}
		// "[" 키를 누르면 화면 확대
		if (Input.GetKeyDown(KeyCode.LeftBracket))
		{
			Camera.main.transform.Translate(0, 0, 1);
		}
		// "]" 키를 누르면 화면 축소
		if (Input.GetKeyDown(KeyCode.RightBracket))
		{ 
			Camera.main.transform.Translate(0, 0, -1);
		}
        if (!_isGameover)
		{
			_surviveTime += Time.deltaTime;
			timeText.text = "Time : " + (int) _surviveTime;
		}
		else
		{
			//게임오버시 R키를 누르면
			if (Input.GetKeyDown(KeyCode.R))
			{
				//SampleScene 씬을 로드
				SceneManager.LoadScene("SampleScene");
			}
		}
    }

	public void	EndGame()
	{
		_isGameover = true;
		gameoverText.SetActive(true);

		float bestTime = PlayerPrefs.GetFloat("BestTime");
		if (_surviveTime > bestTime)
		{
			bestTime = _surviveTime;
			PlayerPrefs.SetFloat("bestTime", bestTime);
			PlayerPrefs.Save();
		}
		recordText.text = "Best Time : " + (int)bestTime;
	}
}
