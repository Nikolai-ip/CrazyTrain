using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;
    public float Angle { set { _angle = value; } }
    [SerializeField] private bool _spawnedByPlayer;
    [SerializeField] private int _ricochetLimit;
    private Rigidbody2D _rb;
    [SerializeField] private float _liveTime;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(_speed * Mathf.Cos(_angle), _speed * Mathf.Sin(_angle));
        transform.Rotate(0, 0, _angle * 180 / Mathf.PI);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > _liveTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(LayerMask.GetMask("Ricochet"), 2))
        {
            if (_ricochetLimit-- == 0)
            {
                Destroy(gameObject);
            }
            UpdateAngle(-_angle);
        }
        else// if (collision.collider.TryGetComponent(out Damagable entity))
        {
            if (collision.gameObject.tag == "Enemy" && !_spawnedByPlayer || collision.gameObject.tag == "Player" && _spawnedByPlayer)
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                _rb.velocity = new Vector2(_speed * Mathf.Cos(_angle), _speed * Mathf.Sin(_angle));
                return;
            }
            Destroy(gameObject);
            //entity.TakeDamage();
        } 
    }

    private void UpdateAngle(float newAngle)
    {
        _angle = newAngle;
        _rb.velocity = new Vector2(_speed * Mathf.Cos(_angle), _speed * Mathf.Sin(_angle));
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.Rotate(0, 0, _angle * 180 / Mathf.PI);
    }
}
