using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI관련 라이브러리
using UnityEngine.SceneManagement;//씬 관리 관련 라이브러리
public class GameManager : MonoBehaviour
{
    public GameObject gameovertxt;
    public Text lifetxt;
    public Text timeText;
    public Text recordText;
    public int lifenum;
    private float surviveTime;
    private bool isGameover;
    void Start()
    {
        lifenum = 3;
        surviveTime = 0;
        isGameover = false;
        timeText.color = Color.blue;
        lifetxt.color = Color.yellow;
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetxt.text = "Life :" + lifenum;//
        if (!isGameover)
        {
            lifetxt.color = Color.yellow;
            surviveTime += Time.deltaTime;
            timeText.text = "Time :" + (int)surviveTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void Endgame()
    {
        isGameover = true;
        gameovertxt.SetActive(true);
        float bestTime = PlayerPrefs.GetFloat("BestTime");
        if (surviveTime > bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime",bestTime);
        }
        recordText.text = "Best Time: " + (int)bestTime;
    }
    
}
