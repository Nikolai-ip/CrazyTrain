using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveState : BaseState
{
    private float _horizontal = 1;
    private float maxSpeed;
    private float minSpeed;
    public float speedInertia = 0.2f;
    [SerializeField] private bool isHide = false;

    private struct CurrentVelocity
    {
        public float X;
        public float Y;
    }

    private CurrentVelocity _currentVelocity;
    public MoveState(StateMachine _sm) : base("MoveState", _sm)
    {

        maxSpeed = _sm.MaxSpeed;
        minSpeed = _sm.MinSpeed;
    }

    public override void Enter()
    {
        base.Enter();


    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _horizontal = (_playerObj.transform.position.x - _collider2D.transform.position.x) / GetDistanceToPlayer();

        if (_distanceToShoot < GetDistanceToPlayer())
        {
            stateMachine.ChangeState(stateMachine._shootState);
            _horizontal = 0;
        }

    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        UpdateVelocity();
        _rigidbody2D.velocity = new Vector2(_currentVelocity.X, _rigidbody2D.velocity.y);

    }

    private void UpdateVelocity()
    {
        if (isHide)
        {
            _currentVelocity.X = 0;
            return;
        }
        if (Mathf.Abs(_horizontal) > 0f)
        {
            _currentVelocity.X = maxSpeed * _horizontal;
        }
        else
        {
            if (Mathf.Abs(_currentVelocity.X * (speedInertia * _horizontal)) < minSpeed)
            {
                _currentVelocity.X = 0f;
            }
            else
            {
                _currentVelocity.X *= speedInertia * _horizontal;
            }
        }
    }


}