using System;
using UnityEngine;

public interface IMove
{
    public float StraightStep { get; }
    public float Rotate { get; }
    public float SideStep { get; }
}

public interface IAction
{
    public bool Fire { get; }
    public bool Reload { get; }
}

public interface IItem
{
    public void Use(GameObject target);
}

public interface IWeapon
{
    public void Fire();
    public bool Reload();
    public int RefillableCount { get; }
    public int ChargedCount { get; }
}

public interface IWeaponHolder
{
    public IWeapon Weapon { get; }
}

public interface IDamageable
{
    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}

public interface ILiving : IDamageable
{
    public float Health { get; }
    public bool IsDead { get; }
    public void Restore(float changeAmount);
    public event Action OnDeath;
}

public interface IInput : IMove, IAction
{
}

public interface IShooter
{
    public IWeapon Weapon { get; set; }
    public IInput Input { get; set; }
}

public interface IGameManager
{
    public bool IsGameover { get; }
}

