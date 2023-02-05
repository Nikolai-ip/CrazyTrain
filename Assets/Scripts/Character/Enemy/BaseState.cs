using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BaseState
{
    public string name;

    protected Collider2D _collider2D;
    protected Rigidbody2D _rigidbody2D;
    protected GameObject _playerObj;
    protected StateMachine stateMachine;
    protected float _distanceToShoot;

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
        _collider2D = stateMachine.Collider2D;
        _rigidbody2D = stateMachine.Rigidbody2D;
        _playerObj = stateMachine.PlayerObj;
        _distanceToShoot = stateMachine.DistanceToShoot;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }


    protected virtual float GetDistanceToPlayer()
    {
        return Mathf.Abs(_collider2D.transform.position.x - _playerObj.transform.position.x);
    }
}