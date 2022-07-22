using UnityEngine;

public partial class Planet : IPlanet
{
    public bool Activate { get; private set; }
    
    public void RotateAxis(float axis)
    {
        transform.rotation = Quaternion.Euler(Vector3.forward * axis);
        SetAxis();
    }

    public void RotateSpeed(float speed)
    {
        _rotateSpeed = speed;
        SetSpeed();
    }

    public GameObject OrbitPoint
    {
        get => _orbitPoint;
        set
        {
            _orbitPoint = value;
            SetOrbitPoint();
        }
    }

    public void OrbitDistance(float distance)
    {
        _distance = distance;
        SetDistance();
    }

    public void OrbitAngularVelocity(float angularVelocity)
    {
        _angularVelocity = angularVelocity;
        SetAngularVelocity();
    }
}

public partial class Planet : MonoBehaviour
{
    private float _rotateSpeed;
    private float _angularVelocity;
    private float _distance;

    private GameObject _orbitPoint = null;
    
    private bool _setAxis;
    private bool _setSpeed;
    private bool _setAngularVelocity;
    private bool _setOrbitPoint;
    private bool _setDistance;
    private bool _setOrbit;

    private void SetAxis()
    {
        _setAxis = true;

        if (_setSpeed)
        {
            gameObject.SetActive(true);
        }
    }

    private void SetSpeed()
    {
        _setSpeed = true;

        if (_setAxis)
        {
            gameObject.SetActive(true);
        }
    }

    private void SetDistance()
    {
        _setDistance = true;

        if (_setAngularVelocity && _setOrbitPoint)
        {
            InitOrbit();
        }
    }
    
    private void SetOrbitPoint()
    {
        _setOrbitPoint = true;

        if (_setDistance && _setAngularVelocity)
        {
            InitOrbit();
        }
    }

    private void SetAngularVelocity()
    {
        _setAngularVelocity = true;

        if (_setDistance && _setOrbitPoint)
        {
            InitOrbit();
        }
    }

    private void InitOrbit()
    {
        if (_orbitPoint != null)
        {
            transform.position = _orbitPoint.transform.position + Vector3.one * _distance;
        }
        _setOrbit = true;
    }
    
    private void Start()
    {
        Activate = false;
        _setOrbit = false;

        Debug.Log(name);
        gameObject.SetActive(false);
    }

    private bool EnableRotateAround => _setOrbit && _orbitPoint != null; 

    private void Update()
    {
        transform.Rotate(Vector3.up * (_rotateSpeed * Time.deltaTime));
        if (EnableRotateAround)
        {
            transform.RotateAround(OrbitPoint.transform.position, OrbitPoint.transform.up, _angularVelocity * Time.deltaTime);
        }
    }
}
