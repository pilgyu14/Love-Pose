using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GunSO")]
public class GunSO : ScriptableObject
{
    public GameObject gunPrefab; // 총 프리팹
    public BulletSO bulletSO; 

    public int bulletCount; // 현재 총알 개수
    [Range(0, 999)] public int ammoCapacity; // 최대 총알 개수
    [Range(0.1f, 2f)] public float standardShotDelay; // 사격 후 딜레이
    [Range(0.1f, 2f)] public float reloadDelay; // 재장전 시간 

    public float shotDelay; // 실제 딜레이 (포즈에 따라 달라짐) 

    public bool isMultiShot; // 여러발 나갈건가 
    public int multiShotCount; // 여러발 쏠경우 몇개 쏠건지

    public float reboundPower; // 반동힘 ( 총마다 있는거 다 더해서 실제 반동에 영향줄거임) 
    [Range(0,10f)] public float shotAngle; // 총알 나가는 각도 

    public GameObject shotEffect; // 쐈을때 이펙트  
    public AudioClip shootClip;
    public AudioClip outOfAmmoClip;
    public AudioClip reloadClip;

    
}
