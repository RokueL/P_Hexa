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

    //=================<      �� ����         >=====================
    public void BlockSpawn()
    {
        if (state == State.NowGame)
        {
            //��Ʈ�� ���� ���Կ� �ִ� ������ ����
            myblock = stackBlock;
            stackBlock.GetComponent<Block>().isStay = true;
            stackBlock.transform.position = spawnPos;

            //�� ��ȯ �� ���Կ� ����
            var blockGO = ObjectPoolManager.Instance.Pool.Get();
            blockGO.transform.position = spawnNextPos;
            blockGO.GetComponent<Block>().isStay = false;
            stackBlock = blockGO;

            //��Ʈ�� ������ ���� ���۳�Ʈ ��������
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
