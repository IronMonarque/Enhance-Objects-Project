using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class Bird_UI_Manager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private TMP_Text txtHealth;
    [SerializeField] private TMP_Text txtScore;
    [SerializeField] private TMP_Text txtHighScore;
    [SerializeField] public TMP_Text nukeCounterUI; // UI element to display nuke count

    [Header("Menu")]
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject lblGameoverText;
    [SerializeField] private TMP_Text txtMenuHighScore;
    
    private BirdPlayer birdPlayer;
    
    private Score_Manager scoreManager;
    public int nukeCount = 0; // Tracks collected nukes

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       scoreManager = BirdGameManager.GetInstance().scoreManager;

        /*scoreManager.OnScoreUpdate.AddListener(UpdateScore);
        scoreManager.OnHighScoreUpdate.AddListener(UpdateHighScore);


        if (scoreManager == null)
        {
            Debug.LogError("Score_Manager instance is null! Check BirdGameManager.");
        }*/

        BirdGameManager.GetInstance().OnGameStart += GameStarted;
        BirdGameManager.GetInstance().OnGameOver += GameOver;
    }

    public void UpdateNukeUI()
    {
        Debug.Log("ui manager received Nuke Count: " + BirdGameManager.GetInstance().nukeCount);
        if (nukeCounterUI != null)
        {
            nukeCounterUI.text = "Nukes: " + BirdGameManager.GetInstance().nukeCount;
        }
    }

    public void UpdateNukeCountUI()
    {
        UpdateNukeUI();
    }

    public void UpdateHealth(float currentHealth)
    {
        txtHealth.SetText(currentHealth.ToString());
        //txtHealth.SetText($"Health: {currentHealth:0}");
    }

    public void UpdateScore()
    {
        txtScore.SetText(scoreManager.GetScore().ToString());        
        //txtScore.SetText($"Score: {scoreManager.GetScore():0}");
    }

    public void UpdateHighScore()
    {
        scoreManager = BirdGameManager.GetInstance().scoreManager;
        txtHighScore.SetText(scoreManager.GetHighScore().ToString());
        txtMenuHighScore.SetText($"High Score : {scoreManager.GetHighScore()}");
    }

    public void GameStarted()
    {
        birdPlayer = BirdGameManager.GetInstance().GetPlayer();
        birdPlayer.health.OnHealthUpdate += UpdateHealth;

        menuCanvas.SetActive(false);
    }

    public void GameOver()
    {
        lblGameoverText.SetActive(true);
        menuCanvas.SetActive(true);
    }
}
