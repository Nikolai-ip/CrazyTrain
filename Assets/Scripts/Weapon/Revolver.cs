using System;
using System.Threading.Tasks;
using UnityEngine;

public class Revolver : Weapon
{
    [SerializeField] RevolverDrum _revolverDrumUI;
    [SerializeField] int _timeDelayBetweenPushBulletMS;
    private DateTime _lastUnsuccesfulPushTime;

    public override void PushBullet()
    {
        if (_revolverDrumUI.CanPushBullet && currentBulletAmount<bulletAmount && (DateTime.Now - _lastUnsuccesfulPushTime).TotalMilliseconds > _timeDelayBetweenPushBulletMS)
        {
            if(_revolverDrumUI.PushBullet())
                currentBulletAmount++;
        } else if (currentBulletAmount < bulletAmount)
        {
            _lastUnsuccesfulPushTime = DateTime.Now;
        }
    }
    public override async void Shoot()
    {
        if (currentBulletAmount > 0  && base.canShoot)
        {
            if (!_revolverDrumUI.IsActive)
            {
                _revolverDrumUI.ShowRevolverDrum(true, currentBulletAmount);
                base.Shoot();
                await Task.Delay(100);
                _revolverDrumUI.PutAwayOneBullet();
                await Task.Delay(400);
                _revolverDrumUI.ShowRevolverDrum(false, currentBulletAmount);
            } else
            {
                _revolverDrumUI.PutAwayOneBullet();
                base.Shoot();
            }
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentBulletAmount = bulletAmount;
    }
}