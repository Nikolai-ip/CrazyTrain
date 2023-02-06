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
    private void Start()
    {
        _animator = GetComponent<Animator>();
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
        GetComponent<StateMachine>().enabled= false;
        GetComponent<EnemyAim>().enabled= false;
        GetComponentInChildren<EnemyRevolver>().GetComponent<SpriteRenderer>().enabled= false;
        GetComponent<BoxCollider2D>().enabled= false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        //Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<Player>().GetComponent<BoxCollider2D>());
        if (new System.Random().Next(0, 100) > 50)
            _animator.SetTrigger("DieHead");
        else
            _animator.SetTrigger("DieGuzno");
        DelayDie();
    }
    private async void DelayDie()
    {
        await Task.Delay(delayDieTime*1000);
        if (gameObject != null && !gameObject.IsDestroyed())
            Destroy(gameObject);
    }
   
}
