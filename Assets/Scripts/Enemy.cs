using Character.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity, Damagable
{
    Animator _animator;
    private StateMachine _stateMachine;
    private EnemyAim _enemyAim;
    private SpriteRenderer _enemyRevolverSR;
    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rb;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _stateMachine = GetComponent<StateMachine>();
        _enemyAim = GetComponent<EnemyAim>();
        _enemyRevolverSR = GetComponentInChildren<EnemyRevolver>().GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }
    public void TakeDamage()
    {
        health--;
        if (health <=0)
        {
            Die();
        }
    }
    protected override void Die()
    {
        _stateMachine.enabled= false;
        _enemyAim.enabled= false;
        _enemyRevolverSR.enabled= false;
        _boxCollider.enabled= false;
        _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        if (new System.Random().Next(0, 100) > 50)
            _animator.SetTrigger("DieHead");
        else
            _animator.SetTrigger("DieGuzno");
        DelayDie();
    }
    private async void DelayDie()
    {
        await Task.Delay(delayDieTime*1000);
        if (gameObject != null)
            Destroy(gameObject);
    }
   
}
