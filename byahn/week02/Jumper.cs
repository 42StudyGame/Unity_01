using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Rigidbody myRigibody;
    // Start is called before the first frame update
    void Start()
    {
        myRigibody.AddForce(0, 500, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
