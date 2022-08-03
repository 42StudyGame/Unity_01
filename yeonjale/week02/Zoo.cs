using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : MonoBehaviour
{ 
    void Start()
    {
        Animal tom = new Animal();
        tom.name = "톰";
        tom.sound = "meow!";
        
        Animal jerry = new Animal();
        jerry.name = "제리";
        jerry.sound = "squeak!";

        jerry = tom;
        jerry.name = "Mickey";
        
        tom.PlaySound();
        jerry.PlaySound();
    }
}
