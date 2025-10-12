using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    private int highScore = 0;
    
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highScoreText != null)
            highScoreText.text = highScore.ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void CheckHighScoreAndReset()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            if (highScoreText != null)
                highScoreText.text = highScore.ToString();
        }

        score = 0;
        UpdateUI();
        ResetAllButtonTimers();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
        
        if (highScoreText != null)
            highScoreText.text = highScore.ToString();
    }

    private void ResetAllButtonTimers()
    {
        foreach (var button in FindObjectsOfType<Button>())
        {
            button.ResetTimer();
        }
    }
}
