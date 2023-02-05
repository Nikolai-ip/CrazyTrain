using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : Entity, Damagable
{
    [SerializeField] private int _initiaHealth;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _sprites;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sprites[_sprites.Count - 1];
    }
    public void TakeDamage()
    {
        float ratio = --health;
        if (ratio < 0) Destroy(gameObject);
        ratio /= _initiaHealth;
        if (ratio > 1) ratio = 1;
        _spriteRenderer.sprite = _sprites[(int)(ratio * (_sprites.Count - 1))];
    }
}
