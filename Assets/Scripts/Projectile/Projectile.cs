using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;
    public float Angle
    { set { _angle = value; } }
    [SerializeField] private bool _spawnedByPlayer;
    [SerializeField] private int _ricochetLimit;
    private Vector2 _velocity;
    private Rigidbody2D _rb;
    [SerializeField] private float _liveTime;
    private float time = 0;
    private AudioSource _flybySource;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(_speed * Mathf.Cos(_angle), _speed * Mathf.Sin(_angle));
        transform.Rotate(0, 0, _angle * 180 / Mathf.PI);
        _velocity = _rb.velocity;

        _flybySource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > _liveTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(LayerMask.GetMask("Ground"), 2))
        {
            if (_ricochetLimit-- == 0)
            {
                Destroy(gameObject);
            }
            Vector2 newVelocity = Vector2.Reflect(_velocity, collision.GetContact(0).normal);
            float angle = Vector2.Angle(_velocity, -collision.GetContact(0).normal);
            Debug.Log(angle);
            if ((angle > 0 && angle < 60) || ( angle < 0 && angle > -60))
            {
                Destroy(gameObject);
            }
            _rb.velocity = newVelocity;
            _velocity = newVelocity;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            if (_flybySource.enabled)
            {
                _flybySource.Play();
            }
        }
        else// if (collision.collider.TryGetComponent(out Damagable entity))
        {
            if (collision.gameObject.tag == "Enemy" && !_spawnedByPlayer || collision.gameObject.tag == "Player" && _spawnedByPlayer)
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                _rb.velocity = _velocity;
                return;
            }
            Destroy(gameObject);
            //entity.TakeDamage();
        }
    }
}