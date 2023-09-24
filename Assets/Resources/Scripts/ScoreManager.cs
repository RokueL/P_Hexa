using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endText;

    int score;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void scoreUp(int count)
    {
        if (count == 3)
            score += 100;
        else if (count == 4)
            score += 200;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        endText.text = ("My Score : " + score.ToString());
    }
}
