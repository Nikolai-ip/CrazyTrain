using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScrolling : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private Transform _cameraTransform;
    private Renderer _renderer;
    private Vector3 _initialPosition;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _initialPosition = transform.position;
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * _scrollSpeed, 1);
        Vector2 offset = new Vector2(x, 0);
        _renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
        transform.position = new Vector3(_cameraTransform.position.x, _initialPosition.y, _initialPosition.z);
    }
}