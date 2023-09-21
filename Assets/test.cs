using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Vector3 checkStartPos;
    Vector3 dirPos;

    // Start is called before the first frame update
    void Start()
    {
        checkStartPos = transform.position;
        testt();
        testt();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void testt()
    {
        checkStartPos = new Vector3(checkStartPos.x - 0.26f, checkStartPos.y + 0.26f);
        dirPos = new Vector3(-0.25f, 0.25f);
        Debug.DrawRay(checkStartPos, dirPos * 0.25f, new Color(1, 1, 0), 10f);
    }
}
