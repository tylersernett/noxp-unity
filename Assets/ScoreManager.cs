using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int highScore;

    private void Awake()
    {
        References.scoreManager = this;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            References.canvas.highScoreText.text = highScore.ToString();
            //save to disk later
            PlayerPrefs.SetInt("highScore", highScore); //held in memory
            PlayerPrefs.Save();//save from memory to disk
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        References.canvas.scoreText.text = score.ToString();
    }
}
