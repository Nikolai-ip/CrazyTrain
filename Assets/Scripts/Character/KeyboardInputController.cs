using UnityEngine;
using UnityEngine.UIElements;

public class KeyboardInputController : MonoBehaviour
{
    [SerializeField] private MouseButton _shootKeyCode;
    [SerializeField] private KeyCode _hideKeyCode;
    private Player _player;
    private HideController _hideController;

    private void Start()
    {
        _player = GetComponent<Player>();
        _hideController = GetComponent<HideController>();
    }

    private void Update()
    {
        CheckShootKeyButtonDown();
        CheckHideKeyButtonDown();
    }

    private void CheckShootKeyButtonDown()
    {
        if (Input.GetMouseButtonDown((int)_shootKeyCode))
        {
            _player.ShootCurrentWeapon();
        }
    }

    private void CheckHideKeyButtonDown()
    {
        if (Input.GetKeyDown(_hideKeyCode))
        {
            _hideController.Hide();
        }
    }
}