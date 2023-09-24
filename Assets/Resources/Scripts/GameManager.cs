using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject blur;

    public static GameManager Instance;
    Block getBlock;
    public TypeSprite[] typeSprite = new TypeSprite[3];
    public enum State
    {
        NowGame,
        GameEnd
    }
    public State state;

    public GameObject blockPrefab;
    public GameObject myblock;

    public bool isDown;

    public GameObject stackBlock;

    public Transform SpawnPos;
    public Transform SpawnNextPos;
    Vector3 spawnPos;
    Vector3 spawnNextPos;

    //=================<      블럭 스폰         >=====================
    public void BlockSpawn()
    {
        if (state == State.NowGame)
        {
            //컨트롤 블럭은 슬롯에 있는 것으로 변경
            myblock = stackBlock;
            stackBlock.GetComponent<Block>().isStay = true;
            stackBlock.transform.position = spawnPos;

            //블럭 소환 후 슬롯에 지정
            var blockGO = ObjectPoolManager.Instance.Pool.Get();
            blockGO.transform.position = spawnNextPos;
            blockGO.GetComponent<Block>().isStay = false;
            stackBlock = blockGO;

            //컨트롤 지정을 위해 컴퍼넌트 가져오기
            getBlock = myblock.GetComponent<Block>();
        }
    }

    public void downButtonPress()
    {
        if (!getBlock.isBlock)
        {
            Time.timeScale = 20f;
        }
    }

    public void downButtonRelease()
    {
        if (!getBlock.isBlock)
        {
            Time.timeScale = 1f;
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

    public void reLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    void Start()
    {
        spawnPos = SpawnPos.transform.position;
        spawnNextPos = SpawnNextPos.transform.position;

        Time.timeScale = 1f;
        //var blockGO = ObjectPoolManager.Instance.Pool.Get();
        //blockGO.transform.position = spawnNextPos;
        //stackBlock = blockGO;


        BlockSpawn();
        
    }

    void Update()
    {
        switch (state)
        {
            case State.NowGame:
                break;
            case State.GameEnd:
                blur.SetActive(true);
                break;
        }
    }
}
