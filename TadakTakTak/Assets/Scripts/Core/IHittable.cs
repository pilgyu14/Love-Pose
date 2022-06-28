using System;
using UnityEngine; 

public class DamageInfo
{


    public Vector3 hitNormal;
    public Vector3 hitPoint; 
}
public interface IHittable
{
    public bool IsEnemy { get; }

    public void GetHit(int damage, GameObject damageDealer); // 맞았을 때 
}
