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
        [SerializeField] private float hideColliderSize = 0.5f;
        private Animator _animator;
        private BoxCollider2D _boxCollider;
        private Vector2 _defaultColliderSize;
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
            _boxCollider = GetComponent<BoxCollider2D>();
            _defaultColliderSize = _boxCollider.size;
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
            var horizontal = context.ReadValue<Vector2>().x;
            var vertical = context.ReadValue<Vector2>().y;
            if (horizontal + vertical > 1)
            {
                _horizontal =
                    (float)Math.Sqrt(2 * Sqr(horizontal) + 2 * Sqr(vertical) - 1);
            }
            else
            {
                _horizontal = horizontal;
            }
        }

        private float Sqr(float a)
        {
            return a * a;
        }
    
        public void Hide(InputAction.CallbackContext context)
        {
            isHide = context.ReadValueAsButton();
            _animator.SetBool("IsHide", isHide);

            if (isHide)
            {
                _boxCollider.size = new Vector2(_defaultColliderSize.x, hideColliderSize);
                _boxCollider.offset = new Vector2(0.0f, (hideColliderSize - _defaultColliderSize.y) / 2.0f);
            }
            else
            {
                _boxCollider.size = _defaultColliderSize;
                _boxCollider.offset = new Vector2(0.0f, 0.0f);
            }
        }
    }
}