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

        jerry = tom;
        jerry.name = "미키";
        
        tom.PlaySound();
        jerry.PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
