using UnityEngine;
using System; 

[Serializable]
public class PoseInfo
{
    public float atk;
    public float bulletSpeed;
    public float shotDelay;
    public float bombRadius;
    public bool isBomb;

    public Vector3 reboundDir;
    public float reboundPowerMultiple;
}
[CreateAssetMenu(menuName = "SO/PoseSO")]
public class PoseSO : ScriptableObject
{
    public Vector3 shotDir; // 공격 방향 
    public PoseInfo poseInfo;
    //public float rebound;  // 반동힘 
    //public float _shotPower; 
    //public bool isBomb; // 터지는가 
    //public float bombRadius; // 폭발 반경 


    public GameObject impactObstacleEffect;
    public GameObject impactEnemyEffect;

    public AudioClip poseClip; 
    //[Range(1, 10)] public int damage; // 공격력
    //[Range(100, 3000)] public int bulletSpeed; // 날아가는 속도 

    //public bool isBumb; // 터지는가 
    //[Range(0, 10)] public float BombRadius; // 폭발 반경  
    //private float lifeTime = 2f;

    //public GameObject impactObstacleEffect; // 장애물에 부딪혔을때 이펙트
    //public GameObject impactEnemyEffect; // 장애물에 부딪혔을때 이펙트



    //public GameObject gunPrefab; // 총 프리팹
    //public BulletSO bulletSO;

    //public int bulletCount; // 현재 총알 개수
    //[Range(0, 999)] public int ammoCapacity; // 최대 총알 개수
    //[Range(0.1f, 2f)] public float shotDelay; // 사격 후 딜레이
    //[Range(0.1f, 2f)] public float reloadDelay; // 재장전 시간 


    //public bool isMultiShot; // 여러발 나갈건가 
    //public int multiShotCount; // 여러발 쏠경우 몇개 쏠건지

    //[Range(0, 10f)] public float shotAngle; // 총알 나가는 각도 

    //public GameObject shotEffect; // 쐈을때 이펙트  
    //public AudioClip shootClip;
    //public AudioClip outOfAmmoClip;
    //public AudioClip reloadClip;
}
