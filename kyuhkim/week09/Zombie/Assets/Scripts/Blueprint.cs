using System;
using System.Threading.Tasks;
using Photon.Pun;
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

public interface IPoolItem : IItem
{
    public IPhotonObjectPool Home { get; set; }
    public void Release();
}

public interface IPhotonPoolItem : IPoolItem
{
    public int Viewid { get; set; }
}

public interface IWeapon
{
    public void Fire();
    public bool Reload();
    public int RefillableCount { get; set; }
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
    public void AddScore(int score);
}

public interface IPhotonObjectPool
{
    public Task SetPrefab(string path);
    public Task<PhotonView> Request();
    public void Release(int key);
    public PhotonView Search(int id);
}

public class PhotonCustomEventCode
{
    public const byte Request = 1;
    public const byte Release = 2;
}
