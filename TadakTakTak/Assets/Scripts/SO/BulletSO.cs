using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/BulletSO")]
public class BulletSO : ScriptableObject
{
    public Bullet bulletPrefab; // 총알 프리팹
    [Range(1, 10)] public int standardDamage; // 공격력
    [Range(10, 3000)] public int standardBulletSpeed; // 날아가는 속도 

    public int damage; // 실제 데미지 (포즈에 따라 달라짐 ) 
    public int bulletSpeed; // 실제 총알 속도 ( 포즈에 따라 달라짐) 

    public bool isBomb; // 터지는가 
    [Range(0,10)]public float BombRadius; // 폭발 반경  
    public float lifeTime = 2f; 

    public GameObject impactObstacleEffect; // 장애물에 부딪혔을때 이펙트
    public GameObject impactEnemyEffect; // 장애물에 부딪혔을때 이펙트

    public void SetData(bool isBomb )
    {

    }
}