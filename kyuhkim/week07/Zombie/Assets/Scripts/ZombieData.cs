using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ZombieData", fileName = "Zombie Data")]
public class ZombieData : ScriptableObject
{
    public float health;
    public float damage;
    public float speed;
    public Color skilColor;
}
