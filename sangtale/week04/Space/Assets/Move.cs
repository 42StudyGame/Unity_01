using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform childTransform;
    
    void Start()
    {
        // 자신의 전역 위치를 (0, -1, 0)으로 변경
        transform.position = new Vector3(0, -1, 0);
        // 자식의 전역 위치를 (0, 2, 0)으로 변경
        childTransform.localPosition = new Vector3(0, 2, 0);
        
        // 자신의 전역 회전을 (0, 0, 30)으로 변경
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
        // 자식의 지역 회전을 (0, 60, 0)으로 변경
        childTransform.localRotation = Quaternion.Euler(new Vector3(0, 60, 0));
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
            childTransform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 0, -180) * Time.deltaTime);
            childTransform.Rotate(new Vector3(0, -180, 0) * Time.deltaTime);
        }
    }
}
