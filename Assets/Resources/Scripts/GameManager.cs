using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TypeSprite[] typeSprite = new TypeSprite[3];
    public enum State
    {
        blockMove,
        endMove,
        GameEnd
    }
    State state;

    public GameObject blockPrefab;


    Vector3 spawnPos = new Vector3(0, 2f, 0);

    //=================<      블럭 스폰         >=====================
    public void BlockSpawn()
    {
        var blockGO = ObjectPoolManager.Instance.Pool.Get();
        blockGO.transform.position = spawnPos;
    }


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

    }

    void Start()
    {
        BlockSpawn();
    }

    void Update()
    {
        switch (state)
        {
            case State.blockMove:
                break;
            case State.endMove:
                break;
            case State.GameEnd: 
                break;
        }
    }
}
