using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloCode3 : MonoBehaviour
{
    void Start()
    {
        float distance = GetDistance(2, 2, 6, 8);
        Debug.Log("(2,2)에서 (6.8)까지의 거리 : " + distance);
    }

    float GetDistance(float x1, float y1, float x2, float y2)
    {
        float width = x2 - x1;
        float height = y2 - y1;

        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);

        return (distance);
    }
}
