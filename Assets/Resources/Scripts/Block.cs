using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Block : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }

    [SerializeField]tiles[] tile = new tiles[3];

    float curTime;
    Vector3 rayPos;
    bool isBlock , isMoveNo;

    void Start()
    {
        curTime = 0;
        tile = GetComponentsInChildren<tiles>();
    }
    #region MOVE
    //=================< ¿· Ã¼Å© ÈÄ ÀÌµ¿         >=====================
    void move()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isBlock)
        {
            moveCheck("A");
            if (!isMoveNo)
            {
                transform.position += new Vector3(-0.5f, 0, 0f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isBlock)
        {
            moveCheck("D");
            if (!isMoveNo)
            {
                transform.position += new Vector3(0.5f, 0f, 0f);
            }
        }
    }
    //=================< ¿· Ã¼Å· È£Ãâ         >=====================
    void moveCheck(string key)
    {
        for(int i = 0; i < tile.Length; i++)
        {
            isMoveNo = tile[i].tilesMoveCheck(key);
            Debug.Log("movechecking : " + isMoveNo);
        }
    }
    //=================< ¹Ø ºí·° Ã¼Å·         >=====================
    void downCheck()
    {
        curTime += Time.deltaTime;
        if (curTime > 0.5f && !isBlock)
        {
            //=================< RayHit Check         >=====================
            rayPos = transform.position + new Vector3 (0, -0.26f, 0f);
            LayerMask mask = LayerMask.GetMask("Block") | LayerMask.GetMask("Ground");
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, 0.4f, mask);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                isBlock = true;
                GameManager.Instance.BlockSpawn();
            }

            //=================< Move Block         >=====================
            if (!isBlock)
            {
                transform.position += new Vector3(0, -0.5f, 0f);
            }
            curTime = 0;
        }
    }
    #endregion

    void Update()
    {
        move();
        //=================< Test Color         >=====================
        if (isBlock)
        {
            for (int i = 0; i < tile.Length; i++) {
                tile[i].end();
            }
        }
    }
    private void FixedUpdate()
    {
        downCheck();
        //=================< Debug Ray         >=====================
        Debug.DrawRay(rayPos, Vector2.down * 0.5f, new Color(1, 0, 0));
    }
}
