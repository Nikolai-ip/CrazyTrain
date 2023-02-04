using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyboardInputController : MonoBehaviour
{
    [SerializeField] private MouseButton _shootKeyCode;
    [SerializeField] private KeyCode _hideKeyCode;
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();   
    }
    void Update()
    {
        CheckShootKeyCodeIsDown();
    }
    private void CheckShootKeyCodeIsDown()
    {
        if (Input.GetMouseButtonDown((int)_shootKeyCode))
        {
            _player.ShootCurrentWeapon();
        }
    }    
}
