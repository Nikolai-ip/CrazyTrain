using System;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyRevolver : Weapon
{
    [SerializeField] RevolverDrum _revolverDrumUI;
    [SerializeField] int _timeDelayBetweenPushBulletMS;
    private DateTime _lastUnsuccesfulPushTime;

    public override void PushBullet()
    {
        if (_revolverDrumUI.CanPushBullet && currentBulletAmount < bulletAmount && (DateTime.Now - _lastUnsuccesfulPushTime).TotalMilliseconds > _timeDelayBetweenPushBulletMS)
        {
            if (_revolverDrumUI.PushBullet())
                currentBulletAmount++;
        }
        else if (currentBulletAmount < bulletAmount)
        {
            _lastUnsuccesfulPushTime = DateTime.Now;
        }
    }
    public override void Shoot()
    {
        base.Shoot();

    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentBulletAmount = bulletAmount;
    }
}