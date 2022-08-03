using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
	public Rigidbody myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
		// x, y, z 방향
        myRigidbody.AddForce(0, 500, 0);
    }
}
