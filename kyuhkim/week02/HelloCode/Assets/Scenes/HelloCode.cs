using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HelloWorld();
        IntroduceSelf();

    }

    private void HelloWorld()
    {
        Debug.Log("Hello world!");
    }

    private void IntroduceSelf()
    {
        string characterName = "kyuhkim";
        char bloodType = 'A';
        int age = 47;
        float height = 180.0f;
        bool isFemale = false;

        Debug.Log("캐릭터 이름 : " + characterName); Debug.Log("혈액형 : " + bloodType); Debug.Log("나이 : " + age);
        Debug.Log("키 : " + height); Debug.Log("여성인가? : " + isFemale);
    }

    private float GetDistance(float x1, float y1, float x2, float y2)
    {
        float width = x2 - x1;
        float height = y2 - y1;

        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);

        return distance;
    }

    private float GetDistance(Vector2 v1, Vector2 v2) => Vector2.Distance(v1, v2);

    private float GetDistance((float x, float y) pos1, (float x, float y) pos2)
    {
        (float width, float height) offset = (pos2.x - pos1.x, pos2.y - pos1.y);

        // float distance = offset.width
        return 0;
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
