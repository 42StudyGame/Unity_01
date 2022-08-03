using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float _rotationSpeed = 60f;
    
    private void Update()
    {
        transform.Rotate(0f, _rotationSpeed * Time.deltaTime, 0f);
    }
}
