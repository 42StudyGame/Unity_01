using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Rigidbody myRigidbody;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.AddForce(0, 500, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
