using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private bool _isForwardRotate;
    private Transform _aimTransform;

    private void Awake()
    {
        _isForwardRotate = true;
        _aimTransform = transform.Find("Aim");
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (_isForwardRotate)
        {
            if (Mathf.Abs(angle) > 90)
            {
                MirrorPlayer();
                _isForwardRotate = false;
                angle = -1 * angle;
            }
        }
        else
        {
            if (Mathf.Abs(angle) < 90)
            {
                MirrorPlayer();
                _isForwardRotate = true;
                angle = -1 * angle;
            }
        }

        _aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void MirrorPlayer()
    {
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}