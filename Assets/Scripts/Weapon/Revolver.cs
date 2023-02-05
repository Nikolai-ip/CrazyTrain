using System;
using System.Threading.Tasks;
using UnityEngine;

public class Revolver : Weapon
{
    [SerializeField] RevolverDrum _revolverDrumUI;
    [SerializeField] private bool _canPutAwayBullet = true;
    [SerializeField] private bool _canPushBullet = true;
    [SerializeField] int _timeDelayBetweenPushBulletMS;
    public override void PushBullet()
    {
        if (_revolverDrumUI.CanPushBullet && currentBulletAmount<bulletAmount && _canPushBullet)
        {
            _revolverDrumUI.PushBullet();
            currentBulletAmount++;
            DelayBetween(_canPushBullet);
        }
    }
    public override void Shoot()
    {
        base.Shoot();
        if (currentBulletAmount>0 && _revolverDrumUI.IsActive && _canPutAwayBullet)
        {
            _revolverDrumUI.PutAwayOneBullet();
            DelayBetween(_canPutAwayBullet);
        }
    }
    private async void DelayBetween(bool flag)
    {
        flag = false;
        await Task.Delay(_timeDelayBetweenPushBulletMS);
        flag = true;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentBulletAmount = bulletAmount;
    }
}