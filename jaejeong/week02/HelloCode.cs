using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloCode : MonoBehaviour
{
	void Start()
	{
		// Hello World 출력
		Debug.Log("Hello World!");

		// 변수 연습하기
		string CharacterName = "라라";
		char bloodType = 'A';
		int age = 17;
		float height = 168.3f;
		bool isFemale = true;
		Debug.Log("캐릭터 이름 : " + CharacterName);
		Debug.Log("혈액형 : " + bloodType);
		Debug.Log("나이: " + age);
		Debug.Log("키 : " + height);
		Debug.Log("여성인가? : " + isFemale);

		// 메서드 연습하기
		float distance = GetDistance(2, 2, 5, 6);
		Debug.Log("(2, 2)에서 (5, 6)까지의 거리 : " + distance);

		// if문
		int love = 80;
		if (love > 90)
		{
			Debug.Log("트루엔딩 : 히로인과 결혼했다!");
		}
		else if (love > 70)
		{
			Debug.Log("굿엔딩 : 히로인과 사귀게 되었다!");
		}
		else
		{
			Debug.Log("배드엔딩 : 히로인에게 차였다.");
		}

		// 논리 연산자
		age = 11;
		if (age > 7 && age < 18)
		{
			Debug.Log("의무 교육을 받고 있습니다.");
		}
		if (age < 13 || age > 70)
		{
			Debug.Log("일을 할 수 없는 나이입니다.");
		}

		// for문
		int i;
		for (i = 0; i < 10; i++)
		{
			Debug.Log(i + "번째 순번입니다.");
		}

		// while문
		while (i < 10)
		{
			Debug.Log(i + "번째 루프입니다.");
			i++;
		}

		// 배열
		int[] students = new int[5] { 100, 90, 80, 70, 60 };
		for (i = 0; i < students.Length; i++)
		{
			Debug.Log((i + 1) + "번 학생의 점수 : " + students[i]);
		}
	}
	float GetDistance(float x1, float y1, float x2, float y2)
	{
		float width = x2 - x1;
		float height;
		
		height = y2 - y1;
		float distance = Mathf.Sqrt(width * width + height * height);
		return distance;
	}
}
