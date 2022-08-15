using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "RedundantJumpStatement")]
public class Move : MonoBehaviour
{
    public Transform childTransform;
    
    void Start()
    {   
        // 자신의 전역 위치를 (0, -1, 0)으로 변경
        transform.position = new Vector3(0, -1, 0);
        // 자식의 지역 위치를 (0, 2, 0)으로 변경
        childTransform.localPosition = new Vector3(0, 2, 0);
        // 자신의 전역 회전을 (0, 0, 30)으로 변경
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
        // 자식의 지역 회전을 (0, 60, 0)으로 변경
        childTransform.localRotation = Quaternion.Euler(new Vector3(0, 60, 0));
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // W키를 누르면 초당 (0, 1, 0)의 속도로 평행이동
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
            // transform.position = transform.position + transform.up * (1 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // S키를 누르면 초당 (0, -1, 0)의 속도로 평행이동
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
            // transform.position = transform.position + transform.up * (-1 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // A키를 누르면 초당 (-1, 0, 0)의 속도로 평행이동
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
            // transform.position = transform.position + transform.right * (-1 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            // D키를 누르면 초당 (1, 0, 0)의 속도로 평행이동
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
            // transform.position = transform.position + transform.right * (1 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // 왼쪽 방향키를 누르면 자신을 초당 (180, 0, 0) 회전
            transform.Rotate(new Vector3(180, 0, 0) * Time.deltaTime);
            // 왼쪽 방향키를 누르면 자식을 초당 (180, 0, 0) 회전
            childTransform.Rotate(new Vector3(180, 0, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // 왼쪽 방향키를 누르면 자신을 초당 (-180, 0, 0) 회전
            transform.Rotate(new Vector3(-180, 0, 0) * Time.deltaTime);
            // 왼쪽 방향키를 누르면 자식을 초당 (-180, 0, 0) 회전
            childTransform.Rotate(new Vector3(-180, 0, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // 왼쪽 방향키를 누르면 자신을 초당 (0, 0, 180) 회전
            transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
            // 왼쪽 방향키를 누르면 자식을 초당 (0, 0, 180) 회전
            childTransform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // 오른쪽 방향키를 누르면 자신을 초당 (0, 0, -180) 회전
            transform.Rotate(new Vector3(0, 0, -180) * Time.deltaTime);
            // 오른쪽 방향키를 누르면 자식을 초당 (0, 0, -180) 회전
            childTransform.Rotate(new Vector3(0, 0, -180) * Time.deltaTime);
        }
        else
            return ;
    }
}
