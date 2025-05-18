using System;
using System.Collections;
using UnityEngine;

public class BirdGameManager : MonoBehaviour

{
    [Header("Game Entities")]
    [SerializeField] private GameObject BirdMeleePrefab, BirdShooterPrefab, BirdMachingunPrefab, BirdExploderPrefab;
    [SerializeField] private Transform[] spawnPositions;

    [Header("Game Variables")]
    [SerializeField] private float enemySpawnRate;
    [SerializeField] private GameObject playerPrefab;

    private GameObject tempEnemy;
    private bool isEnemySpawning;

    private BirdWeapon meleeWeapon = new BirdWeapon("Melee", 1, 0);
    private BirdWeapon shootWeapon = new BirdWeapon("Shoot", 2, 10);
    private BirdWeapon machineGunWeapon = new BirdWeapon("Shoot", 1, 12);
    private BirdWeapon exploderWeapon = new BirdWeapon("Explode", 30, 0);

    public Action OnGameStart;
    public Action OnGameOver;

    public Score_Manager scoreManager;
    private BirdPlayer birdPlayer;
    private bool isPlaying;
    public BirdPickUpSpawner pickupSpawner;

    private static BirdGameManager instance;
    public int nukeCount; //Centralized nuke count

    [SerializeField] private float initialSpawnRate = 1.5f; // Start with a higher rate
    [SerializeField] private float minSpawnRate = 0.2f; // Minimum spawn rate to prevent chaos
    [SerializeField] private float difficultyIncreaseRate = 0.2f; // Gradual difficulty scaling
    private float difficultyTimer = 0f;
    private float spawnTimer = 0f;

    public static BirdGameManager GetInstance()
    {
        return instance;
    }

    void SetSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;
    }

    private void Awake()
    {
        SetSingleton();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (isEnemySpawning)
        {
            difficultyTimer += Time.deltaTime;
            spawnTimer += Time.deltaTime;

            // Every 5 seconds, adjust difficulty dynamically
            if (spawnTimer >= 5f)
            {
                enemySpawnRate *= 0.9f; // Exponential decay for smooth scaling
                enemySpawnRate = Mathf.Max(minSpawnRate, enemySpawnRate);
                spawnTimer = 0f;
            }
        }*/
    }


    void CreateBirdMelee()
    {
        tempEnemy = Instantiate(BirdMeleePrefab);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<BirdEnemy>().weapon = meleeWeapon;
        tempEnemy.GetComponent<BirdMeleeEnemy>().SetMeleeEnemy(2, 0.25f);
    }

    void CreateBirdShooter()
    {
        tempEnemy = Instantiate(BirdShooterPrefab);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<BirdEnemy>().weapon = shootWeapon;
        tempEnemy.GetComponent<BirdShooter>().SetShooterEnemy(3, 1.25f);
    }

    void CreateBirdMachineGunner()
    {
        tempEnemy = Instantiate(BirdMachingunPrefab);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<BirdEnemy>().weapon = machineGunWeapon;
        tempEnemy.GetComponent<BirdMachineGun>().SetMachineGunEnemy(3, 1.25f);
    }

    void CreateBirdExploder()
    {
        tempEnemy = Instantiate(BirdExploderPrefab);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<BirdEnemy>().weapon = exploderWeapon;
        tempEnemy.GetComponent<BirdExploder>().SetExploderEnemy(2, 0.25f);
    }

    //Continously using coroutine
    IEnumerator BirdEnemySpawner()
    {
        while (isEnemySpawning)
        {
            yield return new WaitForSeconds(0.5f / enemySpawnRate);

            int randomIndex = UnityEngine.Random.Range(0, 4); // Assuming 4 enemy types
            switch (randomIndex)
            {
                case 0: CreateBirdMelee(); break;
                case 1: CreateBirdShooter(); break;
                case 2: CreateBirdMachineGunner(); break;
                case 3: CreateBirdExploder(); break;
            }
        }
    }

    public void SetEnemySpawnState(bool status)
    {
        isEnemySpawning = status;

        if (isEnemySpawning)
        {
            difficultyTimer += Time.deltaTime;
            spawnTimer += Time.deltaTime;

            // Every 5 seconds, adjust difficulty dynamically
            if (spawnTimer >= 5f)
            {
                enemySpawnRate -= 0.1f; // Exponential decay for smooth scaling
                enemySpawnRate = Mathf.Max(minSpawnRate, enemySpawnRate);
                spawnTimer = 0f;
            }
        }
    }

    public void NotifyDeath(BirdEnemy enemy)
    {
        pickupSpawner.SpawnPickup(enemy.transform.position);
    }

    public BirdPlayer GetPlayer()
    {
        return birdPlayer;
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    public void StartGame()
    {
        //Create the player
        birdPlayer = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity).GetComponent<BirdPlayer>();
        birdPlayer.OnDeath += StopGame;
        isPlaying = true;

        OnGameStart?.Invoke();
        StartCoroutine(GameStarter());
    }

    IEnumerator GameStarter()
    {
        yield return new WaitForSeconds(2.0f);
        isEnemySpawning = true;

        StartCoroutine(BirdEnemySpawner());
    }

    public void StopGame()
    {
        isEnemySpawning = false;
        scoreManager.SetHighScore();

        StartCoroutine(GameEnding());
    }

    IEnumerator GameEnding()
    {
        isEnemySpawning = false;
        yield return new WaitForSeconds(2);
        isPlaying = false;

        //Delete all enemies
        foreach (BirdEnemy item in FindObjectsByType<BirdEnemy>(FindObjectsSortMode.None))
        {
            Destroy(item.gameObject);
        }

        //Delete all pickups
        foreach (BirdPickup item in FindObjectsByType<BirdPickup>(FindObjectsSortMode.None))
        {
            Destroy(item.gameObject);
        }

        OnGameOver?.Invoke();
    }
}
