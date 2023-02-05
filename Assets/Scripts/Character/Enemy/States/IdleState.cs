using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private float _distanceToPlayer;
    private float _distanceToShot;
    public IdleState(StateMachine _sm) : base("IdleState", _sm)
    {
        _distanceToShot = _sm.DistanceToShoot;
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (GetDistanceToPlayer() < _distanceToShot)
        {
            stateMachine.ChangeState(stateMachine._shootState);
        }

    }


}