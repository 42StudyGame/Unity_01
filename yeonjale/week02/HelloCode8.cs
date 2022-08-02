using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloCode8 : MonoBehaviour
{
    void Start()
    {
        int[] players = new int[3];

        players[0] = 20;
        players[1] = 60;
        players[2] = 100;

        for (int i = 0; i < players.Length; i++)
        {
            Debug.Log("Player" + (i+1) + "'s Health : " + players[i]);
        }
    }
}
