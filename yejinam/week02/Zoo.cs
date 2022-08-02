using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	Animal tom = new Animal();
	tom.name = "톰";
	tom.sound = "냐옹!";

	Animal jerry = new Animal();
	jerry.name = "제리";
	jerry.sound = "찍찍!";

	jerry = tom; // jerry에 tom의값을 복사하는것이 아닌 jerry가 tom이가르키고 있는 값을 똑같이 가르키게됨
	
	jerry.name = "미키"; // jerry가 가리키고있는 tom의 값을 바꿈

	
	tom.PlaySound();
	jerry.PlaySound(); // tom과 jerry가 같은 값을 가르키고있으니 출력 결과도 똑같음

    }
}

