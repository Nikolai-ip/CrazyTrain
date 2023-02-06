using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private float _hitRadius;
    [SerializeField] private float _timeBetweenHit;
    private Animator _animator;
    private bool _canHit=true;
    [SerializeField] private AudioSource _hitSound;
    private void Start()
    {
        _animator= GetComponent<Animator>();
    }
    public void Hit(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && _canHit)
        {
            _hitSound.Play();
            Collider2D collider = Physics2D.OverlapCircle(_hitPoint.position, _hitRadius);
            _animator.SetTrigger("Hit");
            if (collider != null )
            {
                if (collider.TryGetComponent(out Damagable damagable))
                {
                    damagable.TakeDamage();
                }
            }
            DelayBetweenHit();
        }
    }
    private async void DelayBetweenHit()
    {
        _canHit = false;
        await Task.Delay((int)(_timeBetweenHit * 1000));
        _canHit = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_hitPoint.position, _hitRadius);
    }
}
