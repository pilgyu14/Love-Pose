using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    RegularBullet, 
    EnemyRugularBullet,
    Enemy,
    EnemyEffect,
    ObstacleEffect, 
}

public class PoolManager
{    
    public static PoolManager Instance = null;

    private Dictionary<PoolType, Pool<PoolableMono>> _pools = new Dictionary<PoolType, Pool<PoolableMono>>();

    private Transform _trmParent;
    
    public PoolManager(Transform trmParent)
    {
        _trmParent = trmParent;
        //Instance = this;
    }

    public void CreatePool(PoolableMono prefab, PoolType poolType, int count = 10)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, _trmParent, count);
        _pools.Add(poolType, pool);
    }
    
    public PoolableMono Pop(PoolType poolType)
    {
        if(!_pools.ContainsKey(poolType))
        {
            Debug.LogError("Prefab doesnt exist on pool");
            return null;
        }

        PoolableMono item = _pools[poolType].Pop();
        item.Reset();
        return item;
    }

    public void Push(PoolType poolType,PoolableMono obj)
    {
        _pools[poolType].Push(obj);
    }


}
