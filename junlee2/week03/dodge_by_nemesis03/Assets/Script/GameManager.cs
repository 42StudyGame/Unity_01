using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject gameoverText;
    private TextMeshProUGUI lifeText;
    private TextMeshProUGUI timeText;
    private TextMeshProUGUI recordText;
    private float _surviveTime;
    private bool isGameover;
    void Start()
    {
        _surviveTime = 0;
        isGameover = false;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameoverText = GameObject.Find("GameoverText");
        gameoverText.SetActive(false);
        lifeText = GameObject.Find("LifeText").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
        recordText = GameObject.Find("RecordText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!isGameover)
        {
            _surviveTime += Time.deltaTime;
            timeText.text = ("Time : " + ((int)_surviveTime).ToString());
            lifeText.text = ("Life : " + ((int)playerController.life).ToString());
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if (_surviveTime > bestTime)
            bestTime = _surviveTime;
        PlayerPrefs.SetFloat("BestTime",bestTime);
        recordText.SetText("Best Time : " + ((int)bestTime).ToString());
    }
}
