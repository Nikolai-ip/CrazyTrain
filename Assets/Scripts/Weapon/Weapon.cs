using System.Threading.Tasks;
using Character.Player;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, Shootable
{
    [SerializeField] protected float damage;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected Aim characterAim;
    [SerializeField] protected int bulletAmount;
    [SerializeField] protected int shootTimeDelayMs;
    [SerializeField] protected bool canShoot = true;
    protected Animator animator;
    [SerializeField] protected AudioSource gunshot;

    private void Awake()
    {

        characterAim = GetComponentInParent<Aim>();
        Debug.Log(characterAim.gameObject.name);
    }
    public void Shoot()
    {
        if (bulletPrefab != null && canShoot)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = shootPoint.position;
            gunshot.Play();
            if (bullet.TryGetComponent(out Projectile projectile))
            {
                projectile.Angle = characterAim.AngleRad;
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