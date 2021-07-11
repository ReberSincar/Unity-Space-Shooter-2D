using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Text gameOverScore;
    void Start()
    {
        gameOverScore.text = ScoreController.score.ToString();
        ScoreController.score = 0;
    }

}
