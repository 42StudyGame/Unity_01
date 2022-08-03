using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public partial class GameManager : IGameManager
{
    public bool IsGameover { get; private set; }

    public void AddScore(int newScore)
    {
        if (IsGameover)
        {
            return;
        }
        
        _score += newScore;
        scoreText.text = $"Score : {_score}";
    }

    public void OnPlayerDead()
    {
        IsGameover = true;
        gameoverUI.SetActive(true);
    }
}

public partial class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameoverUI;
    private int _score = 0;

    private void Update()
    {
        if (IsGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
