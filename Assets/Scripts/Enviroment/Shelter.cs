using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : MonoBehaviour, Damagable
{
    [SerializeField] private int _initiaHp;
    [SerializeField] private int _hp;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _sprites;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sprites[_sprites.Count - 1];
    }
    public void TakeDamage()
    {
        float ratio = --_hp;
        if (ratio < 0) Destroy(gameObject);
        ratio /= _initiaHp;
        if (ratio > 1) ratio = 1;
        _spriteRenderer.sprite = _sprites[(int)(ratio * (_sprites.Count - 1))];
    }
}
