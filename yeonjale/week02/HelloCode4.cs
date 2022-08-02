using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloCode4 : MonoBehaviour
{
    void Start()
    {
        int health = 80;

        if (health > 90)
        {
            Debug.Log("Health State : Healthy");
        }
        else if (health > 70)
        {
            Debug.Log("Health State : OK");
        }
        else
        {
            Debug.Log("Health State : Critical");
        }
    }
}
