using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractGun : MonoBehaviour,IGun
{
    [SerializeField]
    protected GunSO gunSO;
    [SerializeField]
    protected bool _isEnemyGun;     
    private Camera mainCam; 

    private bool _isDelay; // µÙ∑π¿Ã¡ﬂ 
    private float _curDelayTime = 0; 
    private Transform muzzlePos; 

    private float currentDelay;
    Ray ray;

    public GunSO GunSO => gunSO; 
    void Start()
    {
        muzzlePos    = transform.Find("Muzzle").transform;
    }

    void Update()
    {
        //ShotDelay();
    }

    public void SetGunData(PoseInfo poseInfo)
    {
        gunSO.shotDelay = gunSO.standardShotDelay * poseInfo.shotDelay;
        gunSO.bulletSO.bulletSpeed = (int)(gunSO.bulletSO.standardBulletSpeed * poseInfo.bulletSpeed);
        gunSO.bulletSO.damage = (int)(gunSO.bulletSO.standardDamage * poseInfo.atk);
        gunSO.bulletSO.isBomb = poseInfo.isBomb;
        gunSO.bulletSO.BombRadius = poseInfo.bombRadius;

    }
    private void ShotDelay()
    {
        if (_isDelay == false) // µÙ∑π¿Ã¡ﬂ æ∆¥“∂© ∏Æ≈œ
        {
            return; 
        }

        if (_curDelayTime >= gunSO.standardShotDelay) // µÙ∑π¿Ã¡ﬂ¿Œµ• µÙ∑π¿Ã Ω√∞£ ¥Ÿ√§ø¸¿∏∏È 
        {
            _isDelay = false;
            _curDelayTime = 0; 
            return;
        }
        _curDelayTime += Time.deltaTime; 
    }

    /// <summary>
    ///  √— ΩÓ∞Ì π›µø»˚ ∏Æ≈œ 
    /// </summary>
    /// <returns></returns>
    public float Fire()
    {
        if (_isDelay == true) return 0f;
        SpawnBullet(_isEnemyGun);
        return gunSO.reboundPower; 
    }

    private void SpawnBullet(bool isEnemyBullet)
    {
        Bullet bullet;
        if(isEnemyBullet == false)
        {
            bullet = PoolManager.Instance.Pop(PoolType.RegularBullet) as Bullet;
        }
        else
        {
            bullet = PoolManager.Instance.Pop(PoolType.EnemyRugularBullet) as Bullet;
        }

        bullet.SetPositionAndRotation(muzzlePos.position, muzzlePos.rotation);
        bullet.BulletSO = gunSO.bulletSO; 
        //bullet.transform.position = muzzlePos.position; 
        bullet.SetInfo(gunSO.bulletSO);
        bullet.IsEnemy = isEnemyBullet; 
        //bullet.Shotted();
        StartCoroutine(DelayGun());
    }
    public void Reload()
    {

    }

   public IEnumerator DelayGun()
    {
        _isDelay = true;
        while (_isDelay == true)
        {
            currentDelay += Time.deltaTime;
            if (currentDelay >= gunSO.shotDelay)
            {
                _isDelay = false;
                currentDelay = 0;
                break; 
            }
            yield return null; 
        }
        
    }
}
