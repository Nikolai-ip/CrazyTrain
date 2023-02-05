using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : BaseState
{
    private Weapon _currentWeapon;
    private new float _distanceToShoot;
    public ShootState(StateMachine _sm) : base("ShootState", _sm)
    {
        _distanceToShoot = _sm.DistanceToShoot;
        _currentWeapon = _sm.GetComponentInChildren<Weapon>();
    }

    public override void Enter()
    {
        base.Enter();


    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _currentWeapon.Shoot();
        if (GetDistanceToPlayer() > _distanceToShoot)
        {
            stateMachine.ChangeState(stateMachine._moveState);
        }

    }


}