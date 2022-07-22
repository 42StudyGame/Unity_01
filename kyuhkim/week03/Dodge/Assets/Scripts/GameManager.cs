using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI recordText;

    private float _surviveTime;
    private bool _isGameover;
    private bool _wantToQuit;
    private const string BestTime = "BestTime";
    
    // Start is called before the first frame update
    private void Start()
    {
        _surviveTime = 0;
        _isGameover = false;
        _wantToQuit = false;
    }

    public void EndGame()
    {
        _isGameover = true;
        gameOverText.SetActive(true);
        UpdateBestTime();
    }

    private void UpdateBestTime()
    {
        var bestTime = PlayerPrefs.GetFloat(BestTime);

        if (bestTime < _surviveTime)
        {
            bestTime = _surviveTime;
            PlayerPrefs.SetFloat(BestTime, bestTime);
        }
        
        recordText.text = $"Best Time: {(int)bestTime}";
    }
    
    private void Update()
    {
        if (!_isGameover)
        {
            _surviveTime += Time.deltaTime;
            timeText.text = $"Time: {(int)_surviveTime}";
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_wantToQuit)
                {
                    timeText.text = "will quit";
                    Application.Quit();
                }
                else
                {
                    _wantToQuit = true;
                    timeText.text = "_wantToQuit = true";
                }
            }
            else
            {
                _wantToQuit = false;
                timeText.text = "_wantToQuit = false";
            }
        }
    }
}
