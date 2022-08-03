using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public GameObject gameoverUI;

    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = $"{magAmmo} / {remainAmmo}";
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = $"Score : {score}";
    }

    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = $"Wave : {waves} \nEnemy Left : {count}";
    }

    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
