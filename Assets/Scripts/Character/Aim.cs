using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Aim : MonoBehaviour
{
    protected bool _isForwardRotate;
    protected Transform _aimTransform;
    protected Transform _weaponTransform;
    [SerializeField] private float angle;
    public float AngleRad { get; private set; }

    private void Awake()
    {
        _aimTransform = transform.Find("Aim");
        _weaponTransform = _aimTransform.Find("Revolver");
        InitPosition();
    }

    abstract protected void InitPosition();
    protected void MoveAim(Vector3 positionToMove)
    {
        Vector2 direction = (positionToMove - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        AngleRad = angle / 180 * math.PI;

        if (_isForwardRotate)
        {
            if (Mathf.Abs(angle) > 90)
            {
                MirrorCharacter();
                _isForwardRotate = false;
            }
        }
        else
        {
            if (Mathf.Abs(angle) < 90)
            {
                MirrorCharacter();
                _isForwardRotate = true;
            }
        }

        _aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void MirrorCharacter()
    {
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        _aimTransform.localScale = new Vector3(-1 * _aimTransform.localScale.x, _aimTransform.localScale.y, _aimTransform.localScale.z);
        _weaponTransform.localScale = new Vector3(_weaponTransform.localScale.x, -1 * _weaponTransform.localScale.y, _weaponTransform.localScale.z);
    }
}
