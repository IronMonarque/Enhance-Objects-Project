using UnityEngine;
using UnityEngine.Events;

public class Score_Manager : MonoBehaviour
{
    private int score;
    private int highScore;

    public UnityEvent OnScoreUpdate;
    public UnityEvent OnHighScoreUpdate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        OnHighScoreUpdate.Invoke();
        BirdGameManager.GetInstance().OnGameStart += OnGameStart;
    }

    public int GetScore()
    {
        return score;
    }
    public int GetHighScore()
    {
        return highScore;
    }

    public void IncrementScore()
    {
        score++;
        OnScoreUpdate?.Invoke();

        if (score > highScore) 
        { 
            highScore = score;
            OnHighScoreUpdate?.Invoke();
        }
    }

    public void SetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void OnGameStart()
    {
        score = 0;
        OnScoreUpdate?.Invoke();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
