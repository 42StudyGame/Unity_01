using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private const float _movceSpeed = 5f;
    private const float _rotateSpeed = 180f;
    private IInput _playerInput = null;
    private Rigidbody _playerRigidbody;
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerInput = GetComponent<IInput>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
        // Slide();
        
        _playerAnimator.SetFloat("Move", Mathf.Abs(_playerInput.StraightStep) + Mathf.Abs(_playerInput.SideStep));
    }

    private void Rotate()
    {
        var turn = _playerInput.Rotate * (_rotateSpeed * Time.deltaTime);
        _playerRigidbody.rotation *= Quaternion.Euler(Vector3.up * turn);
    }

    private void Move()
    {
        var moveDistance = transform.forward * (_playerInput.StraightStep * _movceSpeed * Time.deltaTime);
        moveDistance += transform.right * (_playerInput.SideStep * _movceSpeed * Time.deltaTime);
        _playerRigidbody.MovePosition(_playerRigidbody.position + moveDistance);
    }

    // private void Slide()
    // {
    //     var moveDistance = transform.right * (_playerInput.Slide * _movceSpeed * Time.deltaTime);
    //     _playerRigidbody.MovePosition(_playerRigidbody.position + moveDistance);
    // }
}

