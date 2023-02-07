using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerAim : Aim
    {

        protected override void InitPosition()
        {
            _isForwardRotate = true;
        }

        public void ChangeAngle(InputAction.CallbackContext context)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());   
            if (mousePosition != null)
                MoveAim(mousePosition);
        }

    }
}