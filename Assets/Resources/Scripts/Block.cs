using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Block : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }

    public tiles[] tile = new tiles[3];

    float curTime;
    Vector3 rayPos;
    public bool isBlock;
    public bool isMoveNo;
    public bool isStay;

    void Start()
    {
        curTime = 0;
        tile = GetComponentsInChildren<tiles>();
    }
    #region MOVE

    void swap()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isBlock)
        {
            var _tile0 = tile[0].myValue;
            var _tile1 = tile[1].myValue;
            var _tile2 = tile[2].myValue;

            tile[0].myValue = _tile1;
            tile[1].myValue = _tile2;
            tile[2].myValue = _tile0;
        }
    }


    //=================< ¿· Ã¼Å© ÈÄ ÀÌµ¿         >=====================
    void SideMove()
    {
    }
    //=================< ¿· Ã¼Å· È£Ãâ         >=====================
    public void moveCheck(string key)
    {
        for(int i = 0; i < tile.Length; i++)
        {
            isMoveNo = tile[i].tilesMoveCheck(key);
            //Debug.Log("movechecking : " + isMoveNo);
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
                //Debug.Log(hit.collider.name);
                GameManager.Instance.BlockSpawn();
                isBlock = true;
                for (int i = 0; i < tile.Length; i++)
                {
                    tile[i].end();
                }
                //GameManager.Instance.BlockSpawn();
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
        swap();
        SideMove();
        //=================< ÂøÁö         >=====================
        if(isBlock)
        {
            for (int i = 0; i < tile.Length; i++)
            {
                tile[i].end();
            }

        }
    }

    private void LateUpdate()
    {
        if (isStay)
        {
            downCheck();
        }
    }
    private void FixedUpdate()
    {
        //if (isStay)
        //{
        //    downCheck();
        //}
        //=================< Debug Ray         >=====================
        //Debug.DrawRay(rayPos, Vector2.down * 0.5f, new Color(1, 0, 0));
    }
}
