using System.Threading.Tasks;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, Shootable
{
    [SerializeField] protected float damage;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected PlayerAim playerAim;
    [SerializeField] protected int bulletAmount;
    [SerializeField] protected int shootTimeDelayMs;
    [SerializeField] protected bool canShoot = true;
    protected Animator animator;

    public void Shoot()
    {
        if (bulletPrefab != null && canShoot)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = shootPoint.position;
            if (bullet.TryGetComponent(out Projectile projectile))
            {
                projectile.Angle = playerAim.AngleRad;
                animator.SetTrigger("Shoot");
                DelayBetweenShoot();
            }
        }
    }

    protected async void DelayBetweenShoot()
    {
        canShoot = false;
        await Task.Delay(shootTimeDelayMs);
        canShoot = true;
    }
}