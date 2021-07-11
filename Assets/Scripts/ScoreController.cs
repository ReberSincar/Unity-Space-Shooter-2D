using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    Text scoreText;
    public static int score = 0;

    void Start()
    {
      scoreText =  GetComponent<Text>();
    }

    public void IncreaseScore(int addScore) {
        score += addScore;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

}
