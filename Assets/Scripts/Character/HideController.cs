using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    [SerializeField] private bool isHide = false;
    private Animator _animator;
    private void Start()
    {
        GetComponent<PlayerMovement>().onPlayerMove += StopHide;
        _animator = GetComponent<Animator>();
    }
    public void Hide()
    {
        isHide = true;
        _animator.SetBool("IsHide", isHide);
    }
    private void StopHide()
    {
        isHide = false;
        _animator.SetBool("IsHide", isHide);
    }
}
