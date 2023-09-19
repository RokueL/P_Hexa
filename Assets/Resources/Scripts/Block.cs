using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    float curTime;
    Vector3 rayPos;
    bool isBlock;

    void Start()
    {
        curTime = 0;

    }
    void downCheck()
    {
        curTime += Time.deltaTime;
        if (curTime > 0.5f && !isBlock)
        {
            //=================< RayHit Check         >=====================
            rayPos = transform.position + new Vector3 (0, -0.26f, 0f);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, 0.4f, LayerMask.GetMask("Block"));
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                isBlock = true;
            }
            //=================< Move Block         >=====================
            if(!isBlock)
            {
                transform.position += new Vector3(0, -0.5f,0f);
            }
            curTime = 0;
        }
    }
    void Update()
    {
    }
    private void FixedUpdate()
    {
        downCheck();
        //=================< Debug Ray         >=====================
        Debug.DrawRay(rayPos, Vector2.down * 0.5f, new Color(1, 0, 0));
    }
}
