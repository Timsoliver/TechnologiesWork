using UnityEngine;
using TMPro;

public enum GameMode
{
    AimTrainer,
    MazeGame
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("GameMode")]
    [SerializeField] private GameMode currentGameMode = GameMode.AimTrainer; 
    private bool gameStarted = false;
    private bool gameOver = false;
    
    [Header("Scores")]
    public int score = 0;
    private int highScore = 0;
    
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    [Header("Maze Timer")] 
    [SerializeField] private float roundDuration = 60f;
    [SerializeField] private TMP_Text timerText;
    private float timeRemaining = 0f;

    [Header("Maze Mode End UI")] 
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject timeUpUI;
    [SerializeField] private GameObject deathUI;
    
    [Header("End Score Text")]
    [SerializeField] private TMP_Text timeUpScoreText;
    [SerializeField] private TMP_Text deathScoreText;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        if (currentGameMode == GameMode.AimTrainer)
            highScore = PlayerPrefs.GetInt("Aim Trainer HighScore", 0);
        else
            highScore = PlayerPrefs.GetInt("Maze HighScore", 0);
        
        timeRemaining = roundDuration;
        
        if (timeUpUI != null) timeUpUI.SetActive(false);
        if (deathUI != null) deathUI.SetActive(false);

        UpdateScoreUI();
        UpdateHighScoreUI();
        UpdateTimerUI();
    }

    private void Update()
    {
        if (currentGameMode != GameMode.MazeGame)
            return;
        
        if (!gameStarted || gameOver)
            return;
        
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            UpdateTimerUI();
            OnTimeUp();
            return;
        }

        UpdateTimerUI();

        if (playerObject != null && !playerObject.activeInHierarchy)
        {
            OnPlayerDeath();
        }
    }

    public void StartGame()
    {
        if (currentGameMode != GameMode.MazeGame)
            return;
        
        gameStarted = true;
        gameOver = false;

        score = 0;
        timeRemaining = roundDuration;
        if (timeUpUI != null) timeUpUI.SetActive(false);
        if (deathUI != null) deathUI.SetActive(false);

        UpdateScoreUI();
        UpdateTimerUI();
    }
    
    public void AddScore(int amount)
    {
        if (currentGameMode == GameMode.MazeGame && gameOver)
            return;
        
        score += amount;
        UpdateScoreUI();
    }

    public void CheckHighScoreAndReset()
    {
        CheckAndSaveHighScore();
        
        score = 0;
        UpdateScoreUI();
        UpdateHighScoreUI();
        ResetAllButtonTimers();
        
        gameOver = false;
        gameStarted = false;
        timeRemaining = roundDuration;
        UpdateTimerUI();
        
        if (timeUpUI != null) timeUpUI.SetActive(false);
        if (deathUI != null) deathUI.SetActive(false);
    }

    private void OnTimeUp()
    {
        if (gameOver) 
            return;
        if (currentGameMode != GameMode.MazeGame) 
            return;

        gameOver = true;
        gameStarted = false;

        CheckAndSaveHighScore();
        
        if (playerObject != null)
            playerObject.SetActive(false);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        UpdateEndScore();
        
        if (timeUpUI != null) timeUpUI.SetActive(true);
    }

    private void OnPlayerDeath()
    {
        if (gameOver) 
            return;
        if (currentGameMode != GameMode.MazeGame)
            return;
        
        gameOver = true;
        gameStarted = false;

        CheckAndSaveHighScore();
        
        if (playerObject != null) playerObject.SetActive(false);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        UpdateEndScore();
        
        if (deathUI != null) deathUI.SetActive(true);
        
    }

    private void CheckAndSaveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            
            if (currentGameMode == GameMode.AimTrainer)
                PlayerPrefs.SetInt("Aim Trainer HighScore", highScore);
            else
                PlayerPrefs.SetInt("Maze HighScore", highScore);
            
            PlayerPrefs.Save();
        }
        
        UpdateHighScoreUI();
    }

    private void UpdateEndScore()
    {
        string scoreString = score.ToString();
        
        if (timeUpScoreText != null)
            timeUpScoreText.text = scoreString;
        
        if (deathScoreText != null)
            deathScoreText.text = scoreString;
    }
    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }
    
    void UpdateHighScoreUI()
    {
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

    private void UpdateTimerUI()
    {
        if (timerText == null)
            return;

        int totalSeconds = Mathf.CeilToInt(timeRemaining);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
