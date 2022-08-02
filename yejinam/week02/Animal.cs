using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal // MonoBehaviour를 상속하지 않음!
{
    // 동물에 대한 변수
    public string name;
    public string sound;

    // 울음소리를 재생하는 메서드
    public void PlaySound()
    {
	Debug.Log(name + ":" + sound);
    }
}

