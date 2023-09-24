using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class scene : MonoBehaviour
{

    public Button loginButton;

    float time;
    float loadingTime = 1f;

    public void LoadGameScene()
    {
        StartCoroutine(LoadAsynSceneCoroutine("MainScene"));
    }

    IEnumerator LoadAsynSceneCoroutine(string sceneName)
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            time += Time.deltaTime;

            if (operation.progress >= 0.9f && time >= loadingTime)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
