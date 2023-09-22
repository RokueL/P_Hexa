using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    Block getBlock;
    public TypeSprite[] typeSprite = new TypeSprite[3];
    public enum State
    {
        blockMove,
        endMove,
        GameEnd
    }
    State state;

    public GameObject blockPrefab;
    public GameObject myblock;

    public bool isDown;
    bool isEnd;


    public Transform SpawnPos;
    Vector3 spawnPos;

    //=================<      블럭 스폰         >=====================
    public void BlockSpawn()
    {
        if (!isEnd)
        {
            var blockGO = ObjectPoolManager.Instance.Pool.Get();
            blockGO.transform.position = spawnPos;
            myblock = blockGO;
            getBlock = myblock.GetComponent<Block>();
        }
    }

    public void leftButton()
    {
        if (!getBlock.isBlock)
        {
            getBlock.moveCheck("A");
            if (!getBlock.isMoveNo)
            {
                myblock.transform.position += new Vector3(-0.5f, 0, 0f);
            }
        }
    }

    public void rightButton()
    {
        if (!getBlock.isBlock)
        {
            getBlock.moveCheck("D");
            if (!getBlock.isMoveNo)
            {
                myblock.transform.position += new Vector3(0.5f, 0, 0f);
            }
        }
    }

    public void rotationBlock()
    {
        if (!getBlock.isBlock)
        {
            var _tile0 = getBlock.tile[0].myValue;
            var _tile1 = getBlock.tile[1].myValue;
            var _tile2 = getBlock.tile[2].myValue;

            getBlock.tile[0].myValue = _tile1;
            getBlock.tile[1].myValue = _tile2;
            getBlock.tile[2].myValue = _tile0;
        }
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

    GameObject

    void Start()
    {
        spawnPos = SpawnPos.transform.position;

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
