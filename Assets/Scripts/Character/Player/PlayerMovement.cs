using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Transform groundCheck;
        public LayerMask groundLayer;

        private float _horizontal;
        public float maxSpeed = 8f;
        public float minSpeed = 0.1f;
        
        public float speedInertia = 0.2f;

        [SerializeField] private bool isHide = false;
        private Animator _animator;
        [SerializeField] private AudioSource jumpSource;

        public event Action onPlayerMove;

        private struct CurrentVelocity
        {
            public float X;
            public float Y;
        }

        private CurrentVelocity _currentVelocity;
        public float jumpingPower = 16f;
        public float jumpSlowdownCoefficient = 0.2f;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            UpdateVelocity();
            rb.velocity = new Vector2(_currentVelocity.X, rb.velocity.y);
            if (rb.velocity != Vector2.zero)
            {
                onPlayerMove?.Invoke();
            }
        }

        private void UpdateVelocity()
        {
            if (isHide)
            {
                _currentVelocity.X = 0;
                return;
            }
            if (Math.Abs(_horizontal) > 0f)
            {
                _currentVelocity.X = maxSpeed * _horizontal;
            }
            else
            {
                if (Math.Abs(_currentVelocity.X * (speedInertia * _horizontal)) < minSpeed)
                {
                    _currentVelocity.X = 0f;
                }
                else
                {
                    _currentVelocity.X *= speedInertia * _horizontal;
                }
            }
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                jumpSource.Play();
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

        public void Move(InputAction.CallbackContext context)
        {
            _horizontal = context.ReadValue<Vector2>().x;
        }
    
        public void Hide(InputAction.CallbackContext context)
        {
            isHide = context.ReadValueAsButton();
            _animator.SetBool("IsHide", isHide);
        }
    }
}