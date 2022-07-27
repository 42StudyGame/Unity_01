using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Animal
{
	public string name;
	public string sound;
	public void PlaySound()
	{
		Debug.Log($"{name} : {sound}");
	}
}

public class Zoo : MonoBehaviour
{ 
	void Start()
	{
		TomAndJerry();
		ObjectLoss();

	}
	public void TomAndJerry()
	{
		Animal tom = new Animal();
		tom.name = "톰";
		tom.sound = "냐옹!";
		tom.PlaySound();

		Animal jerry = new Animal();
		jerry.name = "제리";
		jerry.sound = "찍찍!";
		jerry.PlaySound();
	}
	public void ObjectLoss()
	{
		Animal tom = new Animal();
		tom.name = "톰";
		tom.sound = "냐옹!";

		Animal jerry = new Animal();
		jerry.name = "제리";
		jerry.sound = "찍찍!";
		
/*
		미아 오브젝트 발생 지점(jerry에게 접근하는 주소를 잃어버린다)
*/
		jerry = tom;
		jerry.name = "미키";

		tom.PlaySound();
		jerry.PlaySound();
/*
		[실행결과]
		> 미키 : 냐옹!
		> 미키 : 냐옹!
*/
	}
}
