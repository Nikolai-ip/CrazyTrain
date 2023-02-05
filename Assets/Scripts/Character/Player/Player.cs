using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Character.Player
{
    public class Player : MonoBehaviour, Damagable
    {
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private delegate void OnHealthChangedDelegate();
        [SerializeField] private OnHealthChangedDelegate onHealthChangedCallback;

        public Weapon CurrentWeapon { private get => _currentWeapon; set => _currentWeapon = value; }

        private void Start()
        {
            Health = 10;
            CurrentWeapon = GetComponentInChildren<Weapon>();
        }

        public void ShootCurrentWeapon()
        {
            CurrentWeapon.Shoot();
        }

        public void TakeDamage()
        {
            PlayerStats.Instance.TakeDamage(1);
            if (PlayerStats.Instance.Health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        public void Heal()
        {
            PlayerStats.Instance.Heal(1);
        }
    }
}