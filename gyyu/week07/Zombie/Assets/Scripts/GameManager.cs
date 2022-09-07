using System;
using Cinemachine;
using UnityEngine;

// 점수와 게임 오버 여부를 관리하는 게임 매니저
public class GameManager : MonoBehaviour {

	private static GameManager m_instance;
	public static GameManager instance
	{
		get
		{
			if (m_instance == null)
				m_instance = FindObjectOfType<GameManager>();
			return m_instance;
		}
	}

	private int score;
	public bool isGameover { get; private set; }

	private void Awake() {
		if (instance != this)
			Destroy(gameObject);
	}
	
	private void Start() {
		FindObjectOfType<PlayerHealth>().onDeath += EndGame;
	}

	private void OnEnable() {
		Cursor.lockState = CursorLockMode.Locked;
	}


	private void Update() {
		if (!isGameover)
			UIManager.instance.UpdateTimeText();
	}

	public void AddScore(int newScore) {
		if (!isGameover)
		{
			score += newScore;
			UIManager.instance.UpdateScoreText(score);
		}
	}

	public void EndGame() {
		isGameover = true;
		UIManager.instance.SetActiveGameoverUI(true);
	}
}