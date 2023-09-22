using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class tiles : MonoBehaviour
{
    public GameObject[] crashGO = new GameObject[15];

    bool isStack, isCheck, isStopCheck, isStopbackCheck, isDownEmpty;
    SpriteRenderer sp;


    Vector3 rayPos;
    Vector3 checkStartPos, dirPos;

    float curTime;
    public int myValue;
    [SerializeField]int count;

    //=================<        키 입력 받은 후 레이 찍고 있으면 트루 전달         >=====================
    public bool tilesMoveCheck(string key)
    {
        LayerMask mask = LayerMask.GetMask("Block") | LayerMask.GetMask("Ground");
        switch (key)
        {
            case "A":
                rayPos = transform.position + new Vector3(-0.26f, -1f, 0f);
                //Debug.DrawRay(rayPos, Vector2.left * 0.4f, new Color(1, 0, 0), 10f);
                RaycastHit2D hitA = Physics2D.Raycast(rayPos, Vector2.left, 0.4f, mask);
                if (hitA.collider != null)
                    return true;
                else
                    return false;
            case "D":
                rayPos = transform.position + new Vector3(0.26f, -1f, 0f);
                //Debug.DrawRay(rayPos, Vector2.right * 0.4f, new Color(1, 0, 0), 10f);
                RaycastHit2D hitD = Physics2D.Raycast(rayPos, Vector2.right, 0.4f, mask);
                if (hitD.collider != null)
                    return true;
                else
                    return false;
            default: 
                return false;
        }
    }

    #region BLOCK_DOWN_CHECK
    //=================<          체킹          >=====================
    public void FirstChecking()
    {
        if (!isCheck)
        {
            for (int i = 0; i < 8; i++)
            {
                checkStartPos = transform.position;
                directionRotation(i);
                RaycastHit2D hit = Physics2D.Raycast(checkStartPos, dirPos, 0.1f, LayerMask.GetMask("Block"));
                //Debug.DrawRay(checkStartPos, dirPos * 0.1f, new Color(1, 0, 0), 10f);
                if (hit.collider != null && hit.collider.GetComponent<tiles>().myValue == myValue)
                {
                    AddBlock(count, this.gameObject);
                    AddBlock(count, hit.collider.gameObject);
                    keepChecking(i);
                }
                else
                {

                }
                if(count <= 3)
                {
                    for(int k = 0; k<crashGO.Length; k++)
                    {
                        crashGO[k] = null;
                    }
                    count = 0;
                }
            }
        }
    }

    void keepChecking(int dir)
    {
        while (true)
        {
            directionRotation(dir);
            RaycastHit2D hit = Physics2D.Raycast(checkStartPos, dirPos, 0.1f, LayerMask.GetMask("Block"));
            if (hit.collider != null && hit.collider.GetComponent<tiles>().myValue == myValue)
            {
                AddBlock(count, hit.collider.gameObject);
            }
            else
            {
                backChecking(dir);
                if (count >= 3)
                {
                    DestroyBlock();
                }
                break;
            }
        }
    }

    void backChecking(int backDir)
    {
        checkStartPos = transform.position;
        backDir = 7 - backDir;

        while (!isStopbackCheck)
        {
            directionRotation(backDir);
            RaycastHit2D hit = Physics2D.Raycast(checkStartPos, dirPos, 0.1f, LayerMask.GetMask("Block"));
            if (hit.collider != null && hit.collider.GetComponent<tiles>().myValue == myValue)
            {
                AddBlock(count, hit.collider.gameObject);
            }
            else
            {
                isStopbackCheck = true;
            }
        }
    }

    //=================<        블럭 추가해서 3개 이상이면 파괴시키기         >=====================
    void AddBlock(int i, GameObject GO)
    {
        crashGO[i] = GO;
        count++;
    }

    //=================<        블럭 없애기         >=====================
    void DestroyBlock()
    {
        for (int i = 0; i < crashGO.Length; i++)
        {
            if (crashGO[i] != null && crashGO[i].activeSelf)
            {
                crashGO[i].SetActive(false);
                crashGO[i] = null;
            }
        }
    }


    //=================<        체킹 방향 지정         >=====================
    public void directionRotation(int dir)
    {
        switch (dir)
        {
            //=================<        윗 방향         >=====================
            case 0:
                checkStartPos = new Vector3(checkStartPos.x - 0.5f, checkStartPos.y + 0.5f);
                dirPos = new Vector3(-0.1f, 0.1f);
                //Debug.Log("0 Direction");
                break;
            case 1:
                checkStartPos = new Vector3( checkStartPos.x + 0f, checkStartPos.y + 0.5f);
                dirPos = new Vector3(0f, 0.1f);
                //Debug.Log("1 Direction");
                break;
            case 2:
                checkStartPos = new Vector3(checkStartPos.x + 0.5f, checkStartPos.y + 0.5f);
                dirPos = new Vector3(0.1f, 0.1f);
                //Debug.Log("2 Direction");
                break;
            //=================<      가운데 방향         >=====================
            case 3:
                checkStartPos = new Vector3(checkStartPos.x - 0.5f, checkStartPos.y +0f);
                dirPos = new Vector3(-0.1f, 0f);
                break;
            case 4:
                checkStartPos = new Vector3(checkStartPos.x + 0.5f, checkStartPos.y + 0f);
                dirPos = new Vector3(0.1f, 0f);
                break;
            //=================<        아랫 방향         >=====================
            case 5:
                checkStartPos = new Vector3(checkStartPos.x  - 0.5f, checkStartPos.y  - 0.5f);
                dirPos = new Vector3(-0.1f, -0.1f);
                break;
            case 6:
                checkStartPos = new Vector3(checkStartPos.x + 0f, checkStartPos.y  - 0.5f);
                dirPos = new Vector3(0f, -0.1f);
                break;
            case 7:
                checkStartPos = new Vector3(checkStartPos.x + 0.5f, checkStartPos.y  - 0.5f);
                dirPos = new Vector3(0.1f, -0.1f);
                break;

        }
    }

    #endregion
    public void end()
    {
        if (!isStack)
        {
            isStack = true;
            isDownEmpty = true;
            if(transform.position.y > 3f)
            {

                return;
            }
            FirstChecking();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        sp = this.gameObject.GetComponent<SpriteRenderer>();
        myValue = Random.Range(0, 5);
    }

    public void downMoveCheck()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (myValue)
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
            case 3:
                this.sp.sprite = GameManager.Instance.typeSprite[3].sprite;
                break;
            case 4:
                this.sp.sprite = GameManager.Instance.typeSprite[4].sprite;
                break;
        }
    }

    private void LateUpdate()
    {
        if (isDownEmpty)
        {

            //=================< RayHit Check         >=====================
            rayPos = transform.position + new Vector3(0, -0.26f, 0f);
            LayerMask mask = LayerMask.GetMask("Block") | LayerMask.GetMask("Ground");
            Debug.DrawRay(rayPos, Vector3.down * 0.1f, new Color(1, 0, 1), 10f);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, 0.1f, mask);
            if (hit.collider != null)
            {
                isStack = true;
            }
            //=================< Move Block         >=====================
            else if (hit.collider == null)
            {
                isStack = false;
                transform.GetComponentInParent<Block>().isBlock = true;
                transform.position += new Vector3(0, -0.5f, 0f);
            }

        }
    }

    private void FixedUpdate()
    {

    }
}
