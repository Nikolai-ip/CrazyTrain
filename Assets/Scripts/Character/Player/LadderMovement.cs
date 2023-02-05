using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LadderMovement : MonoBehaviour
{
    private float _vertical;
    private float _speed = 8f;
    private bool _isLadder;
    private bool _isClimbing;

    [SerializeField] private Rigidbody2D rb;

    void Update()
    {

        if (_isLadder && Mathf.Abs(_vertical) > 0f)
        {
            _isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, _vertical * _speed);
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = false;
            _isClimbing = false;
        }
    }

    public void LadderMove(InputAction.CallbackContext context)
    {
        _vertical = context.ReadValue<Vector2>().y;
    }
}
