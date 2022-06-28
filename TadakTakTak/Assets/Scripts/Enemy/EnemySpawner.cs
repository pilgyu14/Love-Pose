using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemy;

    void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            GameObject enemy = PoolManager.Instance.Pop(PoolType.Enemy).gameObject;
            enemy.transform.position = transform.position;
            yield return new WaitForSeconds(5f);
        }

    }
}
