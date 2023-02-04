using UnityEngine;

namespace Character.Player
{
    public class Player : Entity
    {
        [SerializeField] private Weapon _currentWeapon;

        public Weapon CurrentWeapon { private get => _currentWeapon; set => _currentWeapon = value; }

        private void Start()
        {
            CurrentWeapon = GetComponentInChildren<Weapon>();
        }

        public void ShootCurrentWeapon()
        {
            CurrentWeapon.Shoot();
        }
    }
}