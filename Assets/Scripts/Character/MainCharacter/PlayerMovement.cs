using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float _horizontal;
    public float speed = 8f;
    public float speedInertia = 0.2f;
    private int _accelerationCount = 0;
    public int accelerateFrameLimit = 50;
    private struct CurrentVelocity
    {
        public float X;
        public float Y;
    }

    private CurrentVelocity _currentVelocity;
    public float jumpingPower = 16f;
    public float jumpSlowdownCoefficient = 0.2f;
    private bool _isFacingRight = true;
    
    void Update()
    {
        rb.velocity = new Vector2(_currentVelocity.X, rb.velocity.y);

        //if (!_isFacingRight && _horizontal > 0f)
        //{
        //    Flip();
        //}
        //else if (_isFacingRight && _horizontal < 0f)
        //{
        //    Flip();
        //}
    }

    private void FixedUpdate()
    {
        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        if (Math.Abs(_horizontal) > 0f)
        {
            _currentVelocity.X = _horizontal * speed;
        }
        else
        {
            _currentVelocity.X = rb.velocity.x * speedInertia;
        }
    }

    private void AccelerateCount()
    {
        if (_accelerationCount < accelerateFrameLimit)
        {
            _accelerationCount++;
        }
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            var velocity = rb.velocity;
            velocity = new Vector2(velocity.x, velocity.y * jumpSlowdownCoefficient);
            rb.velocity = velocity;
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        var transform1 = transform;
        var localScale = transform1.localScale;
        localScale.x *= -1f;
        transform1.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _accelerationCount = 0;
        }

        if (context.canceled)
        {
            _accelerationCount = 0;
        }
        _horizontal = context.ReadValue<Vector2>().x;
    }
}
