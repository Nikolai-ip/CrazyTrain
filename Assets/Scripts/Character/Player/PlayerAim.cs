using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerAim : MonoBehaviour
    {
        private bool _isForwardRotate;
        private Transform _aimTransform;
        private Transform _weaponTransform;
        [SerializeField] private float angle;
        public float AngleRad { get; private set; }

        private void Awake()
        {
            _isForwardRotate = true;
            _aimTransform = transform.Find("Aim");
            _weaponTransform = _aimTransform.Find("Revolver");
        }

        public void ChangeAngle(InputAction.CallbackContext context)
        {
            // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition /*rawMousePosition*/);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = (mousePosition - transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            AngleRad = angle / 180 * math.PI;

            if (_isForwardRotate)
            {
                if (Mathf.Abs(angle) > 90)
                {
                    MirrorPlayer();
                    _isForwardRotate = false;
                }
            }
            else
            {
                if (Mathf.Abs(angle) < 90)
                {
                    MirrorPlayer();
                    _isForwardRotate = true;
                }
            }

            _aimTransform.eulerAngles = new Vector3(0, 0, angle);
        }

        private void MirrorPlayer()
        {
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            _aimTransform.localScale = new Vector3(-1 * _aimTransform.localScale.x, _aimTransform.localScale.y, _aimTransform.localScale.z);
            _weaponTransform.localScale = new Vector3(_weaponTransform.localScale.x, -1 * _weaponTransform.localScale.y, _weaponTransform.localScale.z);
        }
    }
}