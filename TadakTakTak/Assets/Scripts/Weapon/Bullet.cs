using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField]
    protected BulletSO _bulletSO; 

    public virtual BulletSO BulletSO
    {
        get => _bulletSO;
        set
        {
            _bulletSO = value;
            damageFactor = _bulletSO.damage; 
        }
    }
    private Rigidbody _rigid;

    [SerializeField]
    protected bool _isEnemy;

    public int damageFactor = 1; // 총알의 데미지 계수
    public bool IsEnemy
    {
        get => _isEnemy;
        set
        {
            _isEnemy = value; 
        }
    }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>(); 
    }

    public void SetInfo(BulletSO bulletSO)
    {
        this.BulletSO = bulletSO; 
    }
    public void Shotted()
    {
        if (_rigid == null)
        {
            _rigid = GetComponent<Rigidbody>();
        }
        _rigid.AddForce(transform.forward * BulletSO.bulletSpeed);
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public override void Reset()
    {

    }
}
