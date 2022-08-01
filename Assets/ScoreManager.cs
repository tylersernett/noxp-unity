using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int highScore;

    float secondsLeftToShowRecentScore;
    int recentScore;
    public float defaultTimeToShowRecentscore;

    private void Awake()
    {
        References.scoreManager = this;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        References.canvas.highScoreText.text = highScore.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        recentScore += amount;
        secondsLeftToShowRecentScore = defaultTimeToShowRecentscore;
        References.canvas.scoreText.text = score.ToString();
        References.canvas.recentScoreText.text = "+" + recentScore.ToString();
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

    private void Update()
    {
        secondsLeftToShowRecentScore -= Time.deltaTime;
        if (secondsLeftToShowRecentScore > 0)
        {
            //otherwise, show the recent score
            References.canvas.recentScoreText.enabled = true;
        }
        else
        {
            //reset recent score when it expires
            recentScore = 0;
            References.canvas.recentScoreText.enabled = false;
        }
    }


}
