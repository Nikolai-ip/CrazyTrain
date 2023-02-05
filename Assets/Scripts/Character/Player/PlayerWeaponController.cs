using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;

    public Weapon CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }

    private void Awake()
    {
        CurrentWeapon = GetComponentInChildren<Weapon>();
    }
    public void ShootCurrentWeapon(InputAction.CallbackContext context)
    {
        if (_currentWeapon != null && context.phase == InputActionPhase.Performed) { _currentWeapon.Shoot(); }
    }
    public void ReloadCurrentWeapon(InputAction.CallbackContext context)
    {
        if (CurrentWeapon != null && context.phase == InputActionPhase.Performed)
        {
            _currentWeapon.Reload(); 
        }
    }
    public void PushBulletInCurrentWeapon(InputAction.CallbackContext context)
    {
        if (CurrentWeapon != null && context.phase == InputActionPhase.Performed)
        {
            _currentWeapon.PushBullet(); 
        }

    }
}
