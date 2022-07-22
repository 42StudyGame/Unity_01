using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : MonoBehaviour
{
    private void Start()
    {
        var tom = new Animal();
        tom.name = "톰";
        tom.sound = "냐옹!";

        var jerry = new Animal
        {
            name = "제리",
            sound = "찍찍!"
        };

        tom.PlaySound();
        jerry.PlaySound();
    }
}
