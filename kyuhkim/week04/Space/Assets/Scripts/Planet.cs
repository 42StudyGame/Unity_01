using UnityEngine;

public partial class Planet : IPlanet
{
    public void SetRotate(Vector3 axis, float speed)
    {
        transform.rotation = Quaternion.Euler(axis);
        _rotateSpeed = speed;
    }

    public void SetOrbit(GameObject orbit, float distance, float angularVelocity)
    {
        _orbitPoint = orbit;
        _distance = distance;
        _angularVelocity = angularVelocity;
        
        InitOrbit();
    }
}

public partial class Planet : MonoBehaviour
{
    private float _rotateSpeed;
    private float _angularVelocity;
    private float _distance;
    private bool _isEnableRotateAround;
    
    private GameObject _orbitPoint = null;

    private void InitOrbit()
    {
        if (_orbitPoint == null)
        {
            return;
        }

        _isEnableRotateAround = true;
        transform.position = _orbitPoint.transform.position + Vector3.forward * _distance;
    }
    
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * (_rotateSpeed * Time.deltaTime));

        if (_isEnableRotateAround)
        {
            transform.RotateAround(_orbitPoint.transform.position, _orbitPoint.transform.up, _angularVelocity * Time.deltaTime);
        }
    }
}
