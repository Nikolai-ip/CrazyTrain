using System;
using Character.Player;
using UnityEngine;

public class InstaKill : MonoBehaviour
{
    public LayerMask killLayer;
    private int _killLayerNumber;
    public Player _damageable;
    private bool _isTouching;

    private void Start()
    {
        _killLayerNumber = (int)Mathf.Log(killLayer.value, 2);
    }

    void Update()
    {
        if (_isTouching)
        {
            //_damageable.InstantlyDie();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _isTouching = col.gameObject.layer.Equals(_killLayerNumber);
    }
}
