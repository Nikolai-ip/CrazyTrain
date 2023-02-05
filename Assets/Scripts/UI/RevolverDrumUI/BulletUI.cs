using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletUI : MonoBehaviour
{
    [SerializeField] private float _angle;
    public bool IsEnable { get; private set; }
    public void Enable()
    {
        IsEnable = true;
        gameObject.SetActive(true);
    }
    public void Disable()
    {
        IsEnable = false;
        gameObject.SetActive(false);
    }
}
