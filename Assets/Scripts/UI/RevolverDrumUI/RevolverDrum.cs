using Character.Player;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class RevolverDrum : MonoBehaviour
{
    [SerializeField] BulletUI[] _bullets;
    [SerializeField] private float _rotateSpeed;
    private Rigidbody2D _rb;
    [SerializeField] GameObject RevolverDrumObject;
    [SerializeField] private bool _canPushBullet=false;
    public event Action<bool> onCanPushBulletChanged;
    private AudioSource _pushBulletAudio;

    public bool IsActive { get;private set; }
    public bool CanPushBullet {
        get { return _canPushBullet; }
        private set
        {
            _canPushBullet = value;
            onCanPushBulletChanged?.Invoke(_canPushBullet);
        } }
    [SerializeField] private float _pushBulletBorder;
    void Start()
    {
        _pushBulletAudio = GetComponent<AudioSource>(); 
        _rb = GetComponent<Rigidbody2D>();
        RevolverDrumObject.SetActive(false);
        PlayerWeaponController player = FindObjectOfType<PlayerWeaponController>();
        player.CurrentWeapon.onReloadButtonIsPressed += ShowRevolverDrum;
    }
    private void FixedUpdate()
    {
        _rb.rotation += _rotateSpeed;
        CanPushBullet = IsCanPushBullet();
        if (_rb.rotation > 360)
            _rb.rotation -= 360;
    }
    private bool IsCanPushBullet()
    {
        return _rb.rotation % 60 > 60 - _pushBulletBorder || _rb.rotation % 60 < _pushBulletBorder;
    }

    public void ShowRevolverDrum(bool isReloading, int bulletAmount)
    {
        SetAlphaChanel(isReloading);
        for (int i = 0; i < bulletAmount; i++)
        {
            _bullets[i].Enable();
        }
        for (int i = bulletAmount; i < _bullets.Length; i++)
        {
            _bullets[i].Disable();
        }
    }
    private void SetAlphaChanel(bool isReloading)
    {
        if (isReloading) { RevolverDrumObject.SetActive(true); IsActive = true; }
        else { RevolverDrumObject.SetActive(false); IsActive = false; }

    }
    public bool PushBullet()
    {
        if (!_bullets[((int)(_rb.rotation + 10) / 60) % 6].IsEnable)
        {
            _bullets[((int)(_rb.rotation + 10) / 60) % 6].Enable();
            _pushBulletAudio.Play();
            return true;
        }
        return false;
    }
    public void PutAwayOneBullet()
    {
        foreach (var bullet in _bullets)
        {
            if (bullet.IsEnable == true) { bullet.Disable(); return; }
        }
    }

}
