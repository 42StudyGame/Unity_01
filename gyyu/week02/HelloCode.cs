using System;
using UnityEngine;

public class HelloCode : MonoBehaviour
{
    void Start()
    {
        // HelloWorld();
        // float distance = GetDistance(2, 2, 5, 6);
        // MultipleEnding(100);
        // ForLoop();
        // WhileLoop();
        StudentScore();
    }

    void HelloWorld()
    {
        Debug.Log("Hello World!");
        string characteName = "라라";
        char bloodType = 'A';
        int age = 17;
        float height = 168.3f;
        bool isFemale = true;

        Debug.Log("캐릭터 이름 : " + characteName);
        Debug.Log("혈액형 : " + bloodType);
        Debug.Log("나이 : " + age);
        Debug.Log("키 : " + height);
        Debug.Log("여성인가? : " + isFemale);
    }

    float GetDistance(float x1, float y1, float x2, float y2)
    {
        float width = x2 - x1;
        float height = y2 - y1;

        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);
        Debug.Log("(" + x1 + "," + y1 + ")에서 (" + x2 + "," + y2 + ")까지의 거리 : " + distance);
        return distance;
    }

    void MultipleEnding(int love)
    {
        Debug.Log("love : " + love);
        
        if (love > 90)
            Debug.Log("트루엔딩 : 히로인과 결혼했다..");
        else if (love > 70)
            Debug.Log("굿엔딩 : 히로인과 사귀게 되었다!");
        else
            Debug.Log("배드엔딩 : 히로인에게 차였다.");
    }

    void ForLoop()
    {
        for (int i = 0; i < 10; i++)
            Debug.Log("for : "+ i + " 번째 순번입니다.");
    }

    void WhileLoop()
    {
        int i = 0;
        while (i < 10)
        {
            Debug.Log( "while : " + i + "번째 루프입니다.");
            i++;
        }
    }

    void StudentScore()
    {
        int[] student = new int[5];

        student[0] = 100;
        student[1] = 90;
        student[2] = 80;
        student[3] = 70;
        student[4] = 60;
        for (int i = 0; i < student.Length; i++)
            Debug.Log((i + 1) + " 번째 학생의 점수: " + student[i]);
    }
}
