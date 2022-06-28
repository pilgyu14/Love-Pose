using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/BulletSO")]
public class BulletSO : ScriptableObject
{
    public Bullet bulletPrefab; // �Ѿ� ������
    [Range(1, 10)] public int standardDamage; // ���ݷ�
    [Range(10, 3000)] public int standardBulletSpeed; // ���ư��� �ӵ� 

    public int damage; // ���� ������ (��� ���� �޶��� ) 
    public int bulletSpeed; // ���� �Ѿ� �ӵ� ( ��� ���� �޶���) 

    public bool isBomb; // �����°� 
    [Range(0,10)]public float BombRadius; // ���� �ݰ�  
    public float lifeTime = 2f; 

    public GameObject impactObstacleEffect; // ��ֹ��� �ε������� ����Ʈ
    public GameObject impactEnemyEffect; // ��ֹ��� �ε������� ����Ʈ

    public void SetData(bool isBomb )
    {

    }
}