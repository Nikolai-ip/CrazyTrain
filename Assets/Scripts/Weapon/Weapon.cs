using System;
using System.Threading.Tasks;
using Character.Player;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, Shootable
{
    [SerializeField] protected float damage;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected Aim playerAim;
    [SerializeField] protected int bulletAmount;
    [SerializeField] protected int currentBulletAmount;
    [SerializeField] protected int shootTimeDelayMs;
    [SerializeField] protected bool canShoot = true;
    protected bool isReloading = true;
    protected Animator animator;
    [SerializeField] protected AudioSource gunshot;
    [SerializeField] protected AudioSource emtyShootSound;
    public event Action<bool, int> onReloadButtonIsPressed;

    public void Awake()
    {
        playerAim = GetComponentInParent<Aim>();
    }

    public virtual void Shoot()
    {

        if (bulletPrefab != null && canShoot && currentBulletAmount>0)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = shootPoint.position;
            gunshot.Play();
            if (bullet.TryGetComponent(out Projectile projectile))
            {
                projectile.Angle = playerAim.AngleRad;
                animator.SetTrigger("Shoot");
                DelayBetweenShoot();
                currentBulletAmount--;
            }
        }
    }

    protected async void DelayBetweenShoot()
    {
        canShoot = false;
        await Task.Delay(shootTimeDelayMs);
        canShoot = true;
    }
    public virtual void Reload()
    {
        onReloadButtonIsPressed?.Invoke(isReloading,currentBulletAmount);
        if (!isReloading)
        {
            isReloading = true;
        }
        else
        {
            isReloading = false;
        }
    }
    public virtual void PushBullet()
    {
        
    }
}