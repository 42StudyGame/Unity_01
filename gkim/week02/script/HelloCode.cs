using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		// string characterName = "라라";
		// char bloodType = 'A';
		// int age = 17;
		// float height = 168.3f;
		// bool isFemale = true;

		// Debug.Log("캐릭터 이름 : " + characterName);
		// Debug.Log("혈액형 : " + bloodType);
		// Debug.Log("나이 : " + age);
		// Debug.Log("키 : " + height);
		// Debug.Log("여성인가? : " + isFemale);
		float distance = GetDistance(2, 2, 5, 6);
		Debug.Log("(2, 2)에서 (5, 6)까지의 거리 : " + distance);
    }

	float GetDistance(float x1, float y1, float x2, float y2)
	{
		float width = x2 - x1;
		float height = y2 - y1;

		float distance = width * width + height * height;
		distance = Mathf.Sqrt(distance);

		return distance;
	}
}
