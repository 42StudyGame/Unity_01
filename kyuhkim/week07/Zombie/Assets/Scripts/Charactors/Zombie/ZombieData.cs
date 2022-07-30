using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ZombieData", fileName = "Zombie Data")]
public class ZombieData : ScriptableObject
{
    public float health = 100f;
    public float damage = 20f;
    public float speed = 2f;
    public Color skilColor = Color.white;
}
