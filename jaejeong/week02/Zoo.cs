using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : MonoBehaviour
{
	void Start()
	{
		Animal tom = new Animal();
		tom.name = "톰";
		tom.sound = "냐옹!";

		tom.PlaySound();
	}
	void Update()
	{
		
	}
}
