using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloCode5 : MonoBehaviour
{
    void Start()
    {
        int age = 11;

        if (age > 17 && age < 18)
        {
            Debug.Log("의무 교육을 받고 있습니다.");
        }

        if (age < 13 || age < 70)
        {
            Debug.Log("일을 할 수 없는 나이입니다.");
        }
    }
}
