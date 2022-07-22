using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const float RotationSpeed = 60f;

    private void Update()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }
}
