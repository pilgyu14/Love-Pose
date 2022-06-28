using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [Header("적 스탯")]
    public int maxHP;
    public int attackPower;
    public float attackDelay;
    public bool isBomb;


    [Header("NavMeshAgent 변수")]
    public float maxSpeed; // 최대 이동 속도 
    public float angularSpeed; // 회전 속도 
    public float acceleration; // 가속도 
    public float stoppingDistance; // 타겟과의 거리제한 
    public bool isAutoBreaking; // 목적지에 도달했을 때 감속하면서 갈 것인가 / False일시 자기 속도 주체 못함 

    [Header("사운드 클립")]
    public AudioInfo walkClip;
    public AudioInfo damagedClip;
    public AudioInfo dieClip;
}
