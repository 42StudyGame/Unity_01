using System;
using UnityEngine;


public partial class GameManager : IGameManager
{
    public bool IsGameover { get; private set; }

    public void AddScore(int score)
    {
        if (IsGameover)
        {
            return;
        }
        
        _score += score;
        _uiManager.UpdateScoreText(_score);
    }
}

public partial class GameManager : MonoBehaviour
{
    private int _score = 0;
    private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        FindObjectOfType<PlayerHealth>().OnDeath += EndGame;
    }

    private void EndGame()
    {
        IsGameover = true;
        _uiManager.SetActiveGameoverUI(true);
    }
}
