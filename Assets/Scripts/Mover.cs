using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private Vector2 _moveDirection;
    private float _speed;
    private Rigidbody2D _rigidbody;

    public void SetMoveDirection(Vector2 direction)
    {
        _moveDirection = direction.normalized;
    }

    public void SetSpeed(float moveSpeed)
    {
        _speed = moveSpeed;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity =  _speed * _moveDirection;
    }
}
