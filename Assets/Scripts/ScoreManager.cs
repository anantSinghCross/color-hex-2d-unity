using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public Text endScoreText;
    private int highScore = 0;
    public Text highScoreText;
    public Text BEST;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log(highScore);
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
            BEST.text = "NEW BEST !";
        }
        highScoreText.text = PlayerPrefs.GetInt("HighScore", score).ToString();
    }
    
}
