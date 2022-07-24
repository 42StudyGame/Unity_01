using UnityEngine;

public static class PlanetInfo
{
    public const float EarthAxis = 23.5f;
    public const float MoonAxis = 6.688f;
    public const float Speed = 50;
}

public interface IRotate
{
    public void SetRotate(Vector3 axis, float speed);
}

public interface IOrbit
{
    public void SetOrbit(GameObject orbit, float distance, float angularVelocity);
}

public interface IPlanet : IRotate, IOrbit 
{
}
