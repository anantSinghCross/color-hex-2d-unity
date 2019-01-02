using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public Text endScoreText;
    private int highScore = 0;
    public Text highScoreText;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log(highScore);
        //highScore = 0;
    }

    public void increaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        
    }

    public void correctScore()
    {
        endScoreText.text = score.ToString();
        if (highScore < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            
            //Debug.Log("highscore = " + score + "/n (HighScore, score);" + PlayerPrefs.GetInt("HighScore", score));
        }
        highScoreText.text = highScore.ToString();
    }
    
}
