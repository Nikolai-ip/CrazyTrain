using Character.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowForPlayer : MonoBehaviour
{
    [SerializeField] private float _dx;
    [SerializeField] private float _dy;
    private Transform _playerTr;
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        _playerTr = player.transform;
    }
    void Update()
    {
        transform.position = new Vector3(_playerTr.position.x + _dx, _playerTr.position.y + _dy);
    }
}
