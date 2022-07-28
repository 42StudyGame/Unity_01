using System;
using System.Threading.Tasks;
using UnityEngine;

public class GalaxyManager : MonoBehaviour
{
    public GameObject earth;
    public GameObject moon;
    public GameObject sun;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        Init();
    }

    private async void Init()
    {
        SetPlanet(sun.GetComponent<IPlanet>(), 0, PlanetInfo.Speed * .2f, null, 0, 0);
        SetPlanet(earth.GetComponent<IPlanet>(), PlanetInfo.EarthAxis, PlanetInfo.Speed * 10, sun, 50, 10);
        SetPlanet(moon.GetComponent<IPlanet>(), PlanetInfo.MoonAxis, PlanetInfo.Speed * 10, earth, 2, 1000);
    }

    private static void SetPlanet(IPlanet planet, float axis, float speed, GameObject orbit, float distance, float angularVelocity)
    {
        planet.SetRotate(Vector3.forward * axis, speed);
        planet.SetOrbit(orbit, distance, angularVelocity);
    }

    private void FixedUpdate()
    {
        _mainCamera.transform.LookAt(earth.transform.position, Vector3.up);
    }
}
