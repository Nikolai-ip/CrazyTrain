using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : Aim
{
    private GameObject playerObj = null;
    private Transform _playerTransform;
    private Vector3 _playerPosition;
    protected override void InitPosition()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = playerObj.transform;
        _playerPosition = _playerTransform.position;
        //if (playerObj is null) return;
        _isForwardRotate = (transform.position.x - _playerPosition.x) > 0;
        MoveAim(_playerPosition);

    }

    private void Start()
    {
    }

    private void Update()
    {
        //if (playerObj is null) return;
        MoveAim(_playerTransform.position);
    }

}
