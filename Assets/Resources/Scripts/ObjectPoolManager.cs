using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    public GameObject blockPrefab;
    public int defaultCapacity = 10;
    public int maxPoolSize = 15;


    public IObjectPool<GameObject> Pool { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Init();
    }

    void Init()
    {
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);

        //=================< �ʱ� �̸� ����         >=====================
        for (int i = 0; i < defaultCapacity; i++)
        {
            Block block = CreatePooledItem().GetComponent<Block>();
            block.Pool.Release(block.gameObject);
        }
    }

    //=================< ����         >=====================
    private GameObject CreatePooledItem()
    {
        GameObject poolGO = Instantiate(blockPrefab);
        poolGO.GetComponent<Block>().Pool = this.Pool;
        return poolGO;
    }
    // =================< ��������         >=====================
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // =================< ��ȯ         >=====================
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    // =================< ����         >=====================
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }


}
