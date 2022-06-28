using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PoolingListSO _initList = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("게임매니저가 여러 개입니다");
        }
        Instance = this;

        PoolManager.Instance = new PoolManager(transform); //풀매니저 생성

        CreatePool(); 
    }

    private void CreatePool()
    {
        for(int i = 0; i < _initList.poolList.Count; i++)
        {
            PoolingPair pair = _initList.poolList[i];
            if(pair == null)
            {
                Debug.LogError("pair : NULL");
            }
            if (pair.prefab == null)
            {
                Debug.LogError("pairPrefab : NULL");
            }

            PoolManager.Instance.CreatePool(pair.prefab, (PoolType)i, pair.poolCnt);
        }
    }

}
