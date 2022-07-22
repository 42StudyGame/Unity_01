using System;
using System.Threading.Tasks;
using UnityEngine;

public class GalaxyManager : MonoBehaviour
{
    public GameObject earth;
    public GameObject moon;
    public GameObject sun;

    private Camera _mainCamera;

    private async void Start()
    {
        _mainCamera = Camera.main;
        
        await WaitUntilDeactivate(sun.GetComponent<IPlanet>());
        await WaitUntilDeactivate(earth.GetComponent<IPlanet>());
        await WaitUntilDeactivate(moon.GetComponent<IPlanet>());
        
        Init();
    }
    
    private static async Task WaitUntilDeactivate(IActivate target)
    {
        await Task.Run(() =>
        {
            while (target.Activate)
            {
                Task.Delay(30);
            }
        });
    }

    private void Init()
    {
        SetPlanet(sun.GetComponent<IPlanet>(), 0, PlanetInfo.Speed * .2f, null, 0, 0);
        SetPlanet(earth.GetComponent<IPlanet>(), PlanetInfo.EarthAxis, PlanetInfo.Speed * 10, sun, 50, 10);
        SetPlanet(moon.GetComponent<IPlanet>(), PlanetInfo.MoonAxis, PlanetInfo.Speed, earth, 3, 100);
    }

    private static void SetPlanet(IPlanet planet, float axis, float speed, GameObject orbitPoint, float distance, float angularVelocity)
    {
        planet.RotateAxis(axis);
        planet.RotateSpeed(speed);
        
        planet.OrbitPoint = orbitPoint;
        planet.OrbitDistance(distance);
        planet.OrbitAngularVelocity(angularVelocity);
    }

    private void Update()
    {
        _mainCamera.transform.LookAt(earth.transform.position, Vector3.up);
    }
}
