using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleRevolverDrum : MonoBehaviour
{
    [SerializeField] Color _originalColor;
    [SerializeField] Color _changeColor;
    private SpriteRenderer _sr;
    private void Start()
    {
        _sr= GetComponent<SpriteRenderer>();
        FindAnyObjectByType<RevolverDrum>().onCanPushBulletChanged += ChangeColor;
    }
    private void ChangeColor(bool isChangeColor)
    {
        _sr.color = isChangeColor? _changeColor : _originalColor;
    }
}
