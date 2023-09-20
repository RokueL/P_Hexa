using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class tiles : MonoBehaviour
{
    public GameObject[] crashGO = new GameObject[15];

    bool isStack, isCheck;
    SpriteRenderer sp;

    Vector3 rayPos;
    Vector3 checkStartPos, dirPos;

    public int myValue;
    int count;

    //=================<        키 입력 받은 후 레이 찍고 있으면 트루 전달         >=====================
    public bool tilesMoveCheck(string key)
    {
        switch (key)
        {
            case "A":
                rayPos = transform.position + new Vector3(-0.26f, -1f, 0f);
                Debug.DrawRay(rayPos, Vector2.left * 0.4f, new Color(1, 0, 0), 10f);
                RaycastHit2D hitA = Physics2D.Raycast(rayPos, Vector2.left, 0.4f, LayerMask.GetMask("Block"));
                if (hitA.collider != null)
                    return true;
                else
                    return false;
            case "D":
                rayPos = transform.position + new Vector3(0.26f, -1f, 0f);
                Debug.DrawRay(rayPos, Vector2.right * 0.4f, new Color(1, 0, 0), 10f);
                RaycastHit2D hitD = Physics2D.Raycast(rayPos, Vector2.right, 0.4f, LayerMask.GetMask("Block"));
                if (hitD.collider != null)
                    return true;
                else
                    return false;
            default: 
                return false;
        }
    }
    //=================<          체킹          >=====================
    void FirstChecking()
    {
        if (!isCheck)
        {
            count = 1;
            for (int i = 0; i < 8; i++)
            {
                directionRotation(i, 1);
                Debug.DrawRay(checkStartPos, dirPos * 0.4f, new Color(1, 0, 0), 10f);
                LayerMask mask = LayerMask.GetMask("Block") | LayerMask.GetMask("Ground");
                RaycastHit2D hit = Physics2D.Raycast(checkStartPos, dirPos, 0.4f, mask);
                if (hit.collider != null && hit.collider.gameObject.GetComponent<tiles>().myValue == myValue)
                {
                    AddBlock(0, this.gameObject);
                    keepChecking(i);
                }
                else
                {
                    return;
                }

            }
        }
    }

    void keepChecking(int dir)
    {
        count++;
        directionRotation(dir,count);
        Debug.DrawRay(checkStartPos, dirPos * 0.4f, new Color(1, 0, 0), 10f);
        LayerMask mask = LayerMask.GetMask("Block") | LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(checkStartPos, dirPos, 0.4f, mask);
        if (hit.collider != null && hit.collider.gameObject.GetComponent<tiles>().myValue == myValue)
        {
            AddBlock(count -1, this.gameObject);
            keepChecking(dir);
        }
        else
        {
            isCheck = true;
            if (count >= 2)
            {
                DestroyBlock();
            }
        }

    }

    void AddBlock(int i, GameObject GO)
    {
        crashGO[i] = GO;
    }

    void DestroyBlock()
    {
        for(int i = 0; i<= count; i++)
        {
            Destroy(crashGO[i]);
        }
    }


    //=================<        체킹 방향 지정         >=====================
    public void directionRotation(int dir, int count)
    {
        checkStartPos = transform.position;
        switch (dir)
        {
            //=================<        윗 방향         >=====================
            case 0:
                checkStartPos = new Vector3(checkStartPos.x - (0.26f * count), checkStartPos.y + (0.26f * count));
                dirPos = new Vector3(-0.25f, 0.25f);
                Debug.Log("0 Direction");
                break;
            case 1:
                checkStartPos = new Vector3( checkStartPos.x + 0f, checkStartPos.y + (0.26f * count));
                dirPos = new Vector3(0f, 0.25f);
                Debug.Log("1 Direction");
                break;
            case 2:
                checkStartPos = new Vector3(checkStartPos.x + (0.26f * count), checkStartPos.y + (0.26f * count));
                dirPos = new Vector3(0.25f, 0.25f);
                Debug.Log("2 Direction");
                break;
            //=================<      가운데 방향         >=====================
            case 3:
                checkStartPos = new Vector3(checkStartPos.x - (0.26f * count), checkStartPos.y +0f);
                dirPos = new Vector3(-0.25f, 0f);
                break;
            case 4:
                checkStartPos = new Vector3(checkStartPos.x + (0.26f * count), checkStartPos.y + 0f);
                dirPos = new Vector3(0.25f, 0f);
                break;
            //=================<        아랫 방향         >=====================
            case 5:
                checkStartPos = new Vector3(checkStartPos.x  - (0.26f * count), checkStartPos.y  - (0.26f * count));
                dirPos = new Vector3(-0.25f, -0.25f);
                break;
            case 6:
                checkStartPos = new Vector3(checkStartPos.x + 0f, checkStartPos.y  - (0.26f * count));
                dirPos = new Vector3(0f, -0.25f);
                break;
            case 7:
                checkStartPos = new Vector3(checkStartPos.x + (0.26f * count), checkStartPos.y  - (0.26f * count));
                dirPos = new Vector3(0.25f, -0.25f);
                break;

        }
    }

    public void end()
    {
        if (!isStack)
        {
            isStack = true;
            FirstChecking();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        sp = this.gameObject.GetComponent<SpriteRenderer>();
        myValue = Random.Range(0, 3);
        switch(myValue)
        {
            case 0:
                this.sp.sprite = GameManager.Instance.typeSprite[0].sprite;
                break;
            case 1:
                this.sp.sprite = GameManager.Instance.typeSprite[1].sprite;
                break;
            case 2:
                this.sp.sprite = GameManager.Instance.typeSprite[2].sprite;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
