using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreMenu : MonoBehaviour
{
    public TextMeshProUGUI verdictText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private void OnEnable()
    {
        if (References.thePlayer == null)
        {
            verdictText.text = "DEAD";
        }
        else
        {
            verdictText.text = "You Win!";
        }
        scoreText.text = "Score: " + References.scoreManager.score.ToString("N0");
        highScoreText.text = "Best Score: " + References.scoreManager.highScore.ToString("N0");
    }

}
