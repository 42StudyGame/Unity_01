using UnityEngine;

public static class PlanetInfo
{
    public const float EarthAxis = 23.5f;
    public const float MoonAxis = 6.688f;
    public const float Speed = 10;
}

public interface IRotate
{
    public void RotateAxis(float axis);
    public void RotateSpeed(float speed);
}

public interface IOrbit
{
    public GameObject OrbitPoint { get; set; }
    public void OrbitDistance(float distance);
    public void OrbitAngularVelocity(float speed);
}

public interface IActivate
{
    public bool Activate { get; }
}

public interface IPlanet : IActivate, IRotate, IOrbit
{
}
