using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HelloCode : MonoBehaviour
{ 
	void Start()
	{
		PrintHelloWorld();
		HelloLaLa();
		Debug.Log(GetDistance(42, 30, 21, 15));
		dokidoki(Random.Range(0, 101));
		LegalAge(Random.Range(0, 101));
		ForLoop();
		WhileLoop(3);
		DotDeal(100);
		StudentScore(Random.Range(0, 42));
	}
	public static void PrintHelloWorld()
	{
		Debug.Log("Hello World!");
	}
	public static void HelloLaLa()
	{
        string characterName = "라라";
        char bloodtype = 'A';
        int age = 17;
        float height = 168.3f;
        bool isfemale = true;
      
        Debug.Log("캐릭터 이름 : " + characterName);
        Debug.Log("혈액형 : " + bloodtype);
        Debug.Log("나이 : " + age);
        Debug.Log("키 : " + height);
        Debug.Log("여성인가요 ? : " + isfemale);
	}
	public static float GetDistance(float x1, float y1, float x2, float y2)
	{
		float width = x2 - x1;
		float height = y2 - y1;
		float distance = width * width + height * height;
		distance = Mathf.Sqrt(distance);
		return (distance);
	}
	public static void dokidoki(int love)
	{
		Debug.Log("호감도 : " + love);

		if (love > 70) 
		{
			Debug.Log("굿엔딩: 히로인과 사귀게 되었다!");
		}
		else if (love > 90)
		{
			Debug.Log("트루엔딩: 히로인과 결혼했다!");
		}
		else
		{
			Debug.Log("배드엔딩: 히로인에게 차였다...");
		}
	}
	public static void LegalAge(int age)
	{
		Debug.Log($"나이 : {age}");
		
		if (age > 7 && age < 18)
		{
			Debug.Log("의무 교육을 받고 있습니다.");
		}
		if (age < 13 || age < 70)
		{
			Debug.Log("일을 할 수 없는 나이입니다.");
		}
		if (age < 14)
		{
			Debug.Log("촉촉한 촉법소년입니다.");
		}
	}
	public static void ForLoop()
	{
		for (int i = 0; i < 10; i++)
		{
			Debug.Log($"{i}번째 순번입니다.");
		}
	}
	public static void WhileLoop(int n)
	{
		int i = 0;
		while (i < n)
		{
			Debug.Log($"{i}번째 루프입니다.");
			i++;
		}
	}
	public static void DotDeal(int hp)
	{
		bool isDead = false;

		while (!isDead)
		{
			Debug.Log($"Your hp : {hp}");
			hp -= 33;
			if (hp <= 0)
			{
				isDead = true;
				Debug.Log("YOU DIED");
			}
		}
	}
	public static void StudentScore(int n)
	{
		int[] students = new int[n];

		for (int i = 0; i < students.Length; i++)
		{
			students[i] = Random.Range(0, 101);
			Debug.Log($"{i}번 학생의 점수 {students[i]}");
		}
	}
}