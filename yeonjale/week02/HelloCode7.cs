using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloCode7 : MonoBehaviour
{
    void Start()
    {
        bool isDead = false;
        int hp = 100;

        while (!isDead)
        {
            Debug.Log("Current hp : " + hp);

            hp = hp - 23;

            if (hp <= 0)
            {
                isDead = true;
                Debug.Log("Player is dead..");
            }
        }
    }
}
