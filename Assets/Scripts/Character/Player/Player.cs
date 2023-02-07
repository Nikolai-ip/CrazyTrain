using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Character.Player
{
    public class Player : MonoBehaviour, Damagable
    {
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] UnityEvent _onTakeDamage;

        public Weapon CurrentWeapon { private get => _currentWeapon; set => _currentWeapon = value; }

        private void Start()
        {
            CurrentWeapon = GetComponentInChildren<Weapon>();
        }

        public void ShootCurrentWeapon()
        {
            CurrentWeapon.Shoot();
        }

        public void TakeDamage()
        {
            PlayerStats.Instance.TakeDamage(1);
            _onTakeDamage?.Invoke();
            if (PlayerStats.Instance.Health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        public void Heal()
        {
            PlayerStats.Instance.Heal(1);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bonus bonus))
            {
                Heal();
                Destroy(collision.gameObject);
            }
        }
    }
}