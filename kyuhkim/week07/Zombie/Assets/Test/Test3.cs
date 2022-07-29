using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    public Test test;

    private void Update()
    {
        if (test.life.Equals(0))
        {
            Destroy(gameObject);
        }
    }
}
